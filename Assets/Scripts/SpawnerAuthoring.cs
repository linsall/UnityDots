using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate = 2;
    public int EnemiesToSpawn = 1;
    public float LifetimeEnemy = 5;
    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            Entity MyEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(MyEntity, new Spawner
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = float2.zero,
                NextSpawnTime = 0,
                SpawnRate = authoring.SpawnRate,
                EnemiesToSpawn = authoring.EnemiesToSpawn
            });
            AddComponent(MyEntity, new EnemyLifetime { Value = authoring.LifetimeEnemy });
        }
    }
}
public partial struct EnemyLifetime : IComponentData
{
    public float Value;
}


