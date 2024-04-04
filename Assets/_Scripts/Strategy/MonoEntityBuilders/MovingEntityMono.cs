using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class MovingEntityMono : MonoBehaviour
{
    public Color RandomColor => UnityEngine.Random.ColorHSV();
}

public class MovingEntityBaker : Baker<MovingEntityMono>
{
    public override void Bake(MovingEntityMono authoring)
    {
        var movingEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(movingEntity, new MovingEntityData
        {
            heading = 0f,
            velocity = 0f,
            color = authoring.RandomColor
        });
        AddComponent(movingEntity, new NavigatedEntityData{
            target = float3.zero
        });
    }
}