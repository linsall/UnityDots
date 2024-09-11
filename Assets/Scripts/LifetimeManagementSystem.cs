using Unity.Burst;
using Unity.Entities;
using UnityEngine;




[UpdateInGroup(typeof(LateSimulationSystemGroup), OrderLast= true)]
public partial struct IsDestroyManagement : ISystem
{
    void OnCreate(ref SystemState state)
    {
        
    }
    public void OnUpdate(ref SystemState state)
    {
        var ECB = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        foreach (var (tag,entity) in SystemAPI.Query<IsDestroying>().WithEntityAccess())
        {
            ECB.DestroyEntity(entity);
        }
        state.Dependency.Complete();
        ECB.Playback(state.EntityManager);
        ECB.Dispose();
    }
}

[BurstCompile]
public partial struct LifetimeManagementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Lifetime>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ECB = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        float DeltaTime = SystemAPI.Time.DeltaTime;

        new LifeJob
        {
            ECB = ECB,
            DeltaTime = DeltaTime
        }.Schedule();
        state.Dependency.Complete();
        ECB.Playback(state.EntityManager);
        ECB.Dispose();
    }

}
[BurstCompile]
public partial struct LifeJob : IJobEntity
{
    public EntityCommandBuffer ECB;
    public float DeltaTime;
    [BurstCompile]
    public void Execute(Entity entity, ref Lifetime lifetime)
    {
        lifetime.Value -= DeltaTime;
        if (lifetime.Value <= 0)
        {
            ECB.AddComponent<IsDestroying>(entity);
        }
    }
}