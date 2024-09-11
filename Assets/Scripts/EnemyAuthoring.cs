using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public class EnemyAuthoring : MonoBehaviour
{
    public float MoveSpeed;

    class EnemyAuthoringBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            Entity EnemyEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(EnemyEntity, new EnemyMoveSpeed { Value = authoring.MoveSpeed });
        }
    }
}


public partial struct EnemyMoveSpeed : IComponentData
{
    public float Value;
}

