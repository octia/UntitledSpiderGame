using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct MovingEntityData : IComponentData
{
    public float heading;
    public float velocity;
    public Color color;
}
