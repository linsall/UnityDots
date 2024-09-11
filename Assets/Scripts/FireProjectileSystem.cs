using Unity.Entities;
using Unity.Transforms;


[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct FireProjectileSystem : ISystem
{

    public void OnUpdate(ref SystemState state)
    {
        var ECB = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        foreach (var (ProjectilePrefab, Lifetime, ProjectileTransform) in SystemAPI.Query<ProjectilePrefab, ProjectileLifetime, LocalTransform>().WithAll<FireProjectileTag>())
        {
            var NewProjectile = ECB.Instantiate(ProjectilePrefab.Prefab);
            var ProjTransform = LocalTransform.FromPositionRotation(ProjectileTransform.Position, ProjectileTransform.Rotation);
            ECB.SetComponent(NewProjectile, ProjTransform);
            ECB.AddComponent(NewProjectile, new Lifetime { Value = Lifetime.Value });
        }
        state.Dependency.Complete();
        ECB.Playback(state.EntityManager);
        ECB.Dispose();
    }
}
