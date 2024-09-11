using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

public partial struct ProjectileMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float DeltaTime = SystemAPI.Time.DeltaTime;

        foreach(var (ProjectileTransform,MoveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, ProjectileMoveSpeed>())
        {
            ProjectileTransform.ValueRW.Position += ProjectileTransform.ValueRO.Up() * MoveSpeed.Value * DeltaTime;
        }
    }
}
