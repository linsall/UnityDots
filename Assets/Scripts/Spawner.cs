using Unity.Entities;
using Unity.Mathematics;
public struct Spawner : IComponentData
{
    public Entity Prefab;
    public float2 SpawnPosition; //More Performant Vector2
    public float NextSpawnTime;
    public float SpawnRate;
    public int EnemiesToSpawn;
}
