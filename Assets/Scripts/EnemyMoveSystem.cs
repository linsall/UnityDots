using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
partial struct EnemyMoveSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float DeltaTime = SystemAPI.Time.DeltaTime;
        foreach(var(EnemySpeed,EnemyTransform) in SystemAPI.Query<EnemyMoveSpeed, RefRW<LocalTransform>>())
        {
            EnemyTransform.ValueRW.Position += -EnemyTransform.ValueRO.Up() * EnemySpeed.Value * DeltaTime;
        }
    }
}