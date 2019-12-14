using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class BulletMoveSystem : JobComponentSystem
{
    private EntityQuery group;//查询到特定组件的实体，将其放入这个组中
    
    protected override void OnCreate()
    {
        group = GetEntityQuery(typeof(Translation), typeof(Rotation), ComponentType.ReadOnly<BulletMoveComponent>());
    }
    public struct BulletMoveJob : IJobChunk
    {
        public float dt;

        public ArchetypeChunkComponentType<Translation> TranslationType;
        public ArchetypeChunkComponentType<Rotation> RotationType;
        [ReadOnly] public ArchetypeChunkComponentType<BulletMoveComponent> BulletMoveType;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var chunkPosition = chunk.GetNativeArray(TranslationType);
            var chunkRotation = chunk.GetNativeArray(RotationType);
            var chunkBulletMove = chunk.GetNativeArray(BulletMoveType);
            for (var i = 0; i < chunk.Count; i++)
            {

                var position = chunkPosition[i];
                var rotation = chunkRotation[i];
                var bulletMove = chunkBulletMove[i];

                chunkPosition[i] = new Translation
                {
                    Value = position.Value + math.forward(rotation.Value) * dt * bulletMove.Speed
                };
            }
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var translationType = GetArchetypeChunkComponentType<Translation>();
        var rotationType = GetArchetypeChunkComponentType<Rotation>();
        var rotationSpeedType = GetArchetypeChunkComponentType<BulletMoveComponent>(true);

        var job = new BulletMoveJob()
        {
            TranslationType = translationType,
            RotationType = rotationType,
            BulletMoveType = rotationSpeedType,
            dt = Time.DeltaTime
        };

        return job.Schedule(group, inputDeps);
    }
}

