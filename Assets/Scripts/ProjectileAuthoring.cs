using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ProjectileAuthoring : MonoBehaviour
{
    public float ProjectileSpeed;

    public class ProjectileAuthoringBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            Entity ProjectileEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(ProjectileEntity, new ProjectileMoveSpeed { Value = authoring.ProjectileSpeed });
        }
    }
}
