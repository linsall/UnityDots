using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public float ProjectileLifetime;
    public GameObject ProjectilePrefab;

    class PlayerAuthoringBaker : Baker<PlayerAuthoring>
    {


        public override void Bake(PlayerAuthoring authoring)
        {
            Entity PlayerEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerTag>(PlayerEntity);
            AddComponent<PlayerMoveInput>(PlayerEntity);

            AddComponent(PlayerEntity, new PlayerMoveSpeed { Value = authoring.MoveSpeed });

            AddComponent<FireProjectileTag>(PlayerEntity);
            SetComponentEnabled<FireProjectileTag>(PlayerEntity,false);

            AddComponent(PlayerEntity, new ProjectilePrefab { Prefab = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic) });
            AddComponent(PlayerEntity, new ProjectileLifetime { Value = authoring.ProjectileLifetime });
        }
    }
}

#region PlayerComponents
public struct PlayerMoveInput : IComponentData
{
    public float2 Value;
}
public struct PlayerMoveSpeed : IComponentData
{
    public float Value;
}
public struct PlayerTag : IComponentData { }
#endregion
#region ProjectileComponents
public struct ProjectilePrefab : IComponentData 
{
    public Entity Prefab;
}
public struct ProjectileMoveSpeed : IComponentData
{
    public float Value;
}
public struct FireProjectileTag : IComponentData, IEnableableComponent { }
public struct ProjectileLifetime: IComponentData
{
    public float Value;
}
public struct Lifetime : IComponentData
{
    public float Value;
}
public struct IsDestroying : IComponentData { }
#endregion