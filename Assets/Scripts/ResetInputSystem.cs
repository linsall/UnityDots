using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast =  true)]
[UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
public partial struct ResetInputSystem : ISystem
{

    public void OnUpdate(ref SystemState state)
    {
        var ECB = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        foreach (var (Tag, Entity) in SystemAPI.Query<FireProjectileTag>().WithEntityAccess())
        {
            ECB.SetComponentEnabled<FireProjectileTag>(Entity, false);
        }
        state.Dependency.Complete();
        ECB.Playback(state.EntityManager);
        ECB.Dispose();
    }
}
