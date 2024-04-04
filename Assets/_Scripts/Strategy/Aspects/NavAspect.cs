using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using System.Numerics;

public readonly partial struct NavAspect : IAspect 
{
    private readonly RefRW<LocalTransform> _localTransform;

    private readonly RefRW<MovingEntityData> _entityData;

    private readonly RefRW<NavigatedEntityData> _navData;


    public void SetPosition(float3 localPosition)
    {
        _localTransform.ValueRW.Position = localPosition;
    }

    public void SetDestination(float3 target)
    {
        _navData.ValueRW.target = target;
        _navData.ValueRW.navigateToTarget = true;
    }

    public void MoveTowardsTarget(float deltaTime)
    {
        if (!_navData.ValueRO.navigateToTarget)
        {
            return;
        }


        float3 currentPosition = _localTransform.ValueRO.Position;
        float3 destination = _navData.ValueRO.target;
        float3 nextPosition = currentPosition;
        float stepLength = 1 * deltaTime;

        // rotation
        quaternion currentRotation = _localTransform.ValueRO.Rotation;
        quaternion endRotation = quaternion.LookRotation(destination - currentPosition, _localTransform.ValueRO.Up());    
        quaternion newRotation = currentRotation;
        _localTransform.ValueRW.Rotation = endRotation;
        

        float3 distToTravel = destination - currentPosition;
        float sqrDist = UnityEngine.Vector3.SqrMagnitude(distToTravel);

        if (sqrDist < stepLength)
        {
            nextPosition = destination;
            _navData.ValueRW.navigateToTarget = false; 
        }
        else
        {
            float3 unitDisplacement = math.rotate(newRotation, new float3(0,0,1));
            nextPosition = currentPosition + (unitDisplacement * deltaTime * 30); //speed 30
        }
        _localTransform.ValueRW.Position = nextPosition;

    }

}
