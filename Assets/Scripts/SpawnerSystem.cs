using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;

[BurstCompile]
public partial struct SpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ECB = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        foreach (var(enemyLifetime,spawner) in SystemAPI.Query<EnemyLifetime,RefRW<Spawner>>())
        {

            if(spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {

                for (int i = 0; i < spawner.ValueRO.EnemiesToSpawn; i++)
                {
                    Entity NewEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                    float3 Position = new float3 (spawner.ValueRO.SpawnPosition.x-8.5f+i, 6, 0); //Hardcoded Spawnposition on X as I am too lazy to try and make it fit the screen well based on enemy spawn count, spawning 18 enemies is most fitting with this setup to fit the whole screen
                    state.EntityManager.SetComponentData(NewEntity,LocalTransform.FromPosition(Position));
                    ECB.AddComponent(NewEntity, new Lifetime { Value = enemyLifetime.Value });
                }
                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
                
            }
        }
        state.Dependency.Complete();
        ECB.Playback(state.EntityManager);
        ECB.Dispose();
    }
}