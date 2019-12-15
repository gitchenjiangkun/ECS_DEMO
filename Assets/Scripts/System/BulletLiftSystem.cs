using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

public class BulletLiftSystem : JobComponentSystem
{
    private EntityCommandBufferSystem entityCommandBufferSystem;
    
    protected override void OnCreate()
    {
        entityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    [BurstCompile]
    public struct BulletLiftJob : IJobForEachWithEntity<BulletLiftComponent>
    {
        public float dt;

        [WriteOnly]//只写
        public EntityCommandBuffer.Concurrent CommandBuffer;

        public void Execute(Entity entity, int index, ref BulletLiftComponent bulletLift)
        {
            bulletLift.Value -= dt;
            if (bulletLift.Value <= 0)
            {
                CommandBuffer.DestroyEntity(index, entity);
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var commandBuffer = entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();

        var jobHandle = new BulletLiftJob()
        {
            dt = Time.DeltaTime,
            CommandBuffer = commandBuffer
        }.Schedule(this, inputDeps);

        entityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

        return jobHandle;
    }
}
