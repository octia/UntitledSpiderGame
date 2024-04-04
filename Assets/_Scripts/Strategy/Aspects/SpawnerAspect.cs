using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct SpawnerAspect : IAspect
{
    private readonly RefRO<LocalTransform> _localTransform;

    private readonly RefRW<EntitySpawnerData> _spawnerData;


    public float3 GetTransformPosition()
    {
        return _localTransform.ValueRO.Position;
    }

    
    public bool TickSpawnTimer(float timeToTick)
    {
        float timeLeft = _spawnerData.ValueRO.timeLeft;
        float spawnDelay = _spawnerData.ValueRO.spawnDelay;
        timeLeft -= timeToTick;
        if (timeLeft < 0)
        {
            _spawnerData.ValueRW.timeLeft = spawnDelay;
            return true;
        }
        else
        {
            _spawnerData.ValueRW.timeLeft = timeLeft; // consider removing this ValueRW eventually. This can be done via only setting last spawn time upon spawning a new entity.
            return false;
        }
    }

    public Entity GetEntityToSpawn()
    {
        return _spawnerData.ValueRO.toSpawn;
    }

    public LocalTransform GetSpawnedEntityTransform()
    {
        return new LocalTransform {
            Position = _localTransform.ValueRO.Position,
            Rotation = _localTransform.ValueRO.Rotation,
            Scale = 1
        };
    }

}