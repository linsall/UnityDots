using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public float3 EnemyTransform;


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

