using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

[BurstCompile]
[UpdateAfter(typeof(InputCaptureSystem))]
public partial struct MovingEntitySystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {

    }
    
    public void OnUpdate(ref SystemState state)
    {
        float dTime = SystemAPI.Time.DeltaTime;
        if (InputCaptureSystem.WasCommandIssued)
        {
            new SetTargetJob
            {
                deltaTime = dTime,
                destination = InputCaptureSystem.TargetPosition
            }.ScheduleParallel();
            InputCaptureSystem.WasCommandIssued = false;
        }

        new NavMoveJob
        {
            deltaTime = dTime
        }.ScheduleParallel();

    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}


[BurstCompile]
public partial struct SetTargetJob : IJobEntity
{
    public float deltaTime;
    public float3 destination;

    [BurstCompile]
    void Execute(NavAspect navAspect)
    {
        navAspect.SetDestination(destination);
    }
}


[BurstCompile]
public partial struct NavMoveJob : IJobEntity
{
    public float deltaTime;

    [BurstCompile]
    void Execute(NavAspect navAspect)
    {
        navAspect.MoveTowardsTarget(deltaTime);
    }
}