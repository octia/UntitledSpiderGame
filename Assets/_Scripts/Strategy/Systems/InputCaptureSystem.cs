using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


/// <summary>
/// This is a very temporary way to grab the world location of the right click. I'm doing it just to see something moving on the screen.
/// The long-term plan is yet to be figured out, but will likely use a singleton aspect - to ensure thread safety (and preformance).
/// 
/// While I was rewriting this class, I realised it doesn't even need to be a system, in its current form. I am keeping the system part around, to allow the [UpdateAfter(typeof(InputCaptureSystem))] on MovingEntitySystem to work, and because I'll be actually using OnUpdate eventually.
/// </summary>
public partial class InputCaptureSystem : SystemBase
{
    public static bool WasCommandIssued;
    public static float3 TargetPosition;

    private float3 _previousTargetPosition;

    private bool _callbackAdded = false;

    protected override void OnCreate()
    {
        WasCommandIssued = false;
        TargetPosition = float3.zero;
        _previousTargetPosition = float3.zero;
    }

    private void OnRightClickHappened(Vector3 position)
    {
        TargetPosition = position;

        float3 distancesBetweenTargets = TargetPosition - _previousTargetPosition;
        float distanceSQ = 0;
        for (int i = 0; i < 3; i++)
        {
            distanceSQ += distancesBetweenTargets[i] * distancesBetweenTargets[i];
        }
        if (distanceSQ >= 1)
        {
            WasCommandIssued = true;
            _previousTargetPosition = TargetPosition;
        }
    }

    protected override void OnUpdate()
    {
        if (!_callbackAdded)
        {
            if (FloorRayGun.Instance)
            {
                FloorRayGun.Instance.RightClickedGround.AddListener(OnRightClickHappened);
                _callbackAdded = true;
            }        
        }
    }
    
}
