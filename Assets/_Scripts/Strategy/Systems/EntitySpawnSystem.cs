using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateBefore(typeof(InputCaptureSystem))]
public partial struct EntitySpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state){}
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dTime = SystemAPI.Time.DeltaTime;
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        new EntitySpawnJob
        {
            ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
            deltaTime = dTime 
        }.ScheduleParallel();
        
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state){}

}


[BurstCompile]
public partial struct EntitySpawnJob : IJobEntity
{
    public float deltaTime;
    public EntityCommandBuffer.ParallelWriter ecb;

    [BurstCompile]
    void Execute(SpawnerAspect spawnerAspect, [EntityIndexInChunk]int sortKey)
    {
        
        if (spawnerAspect.TickSpawnTimer(deltaTime))
        {
            var newEntity = ecb.Instantiate(sortKey, spawnerAspect.GetEntityToSpawn());
            ecb.SetComponent(sortKey, newEntity, spawnerAspect.GetSpawnedEntityTransform());
        }
    }
}