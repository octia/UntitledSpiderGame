using Unity.Entities;
using Unity.Mathematics;

public partial struct NavigatedEntityData : IComponentData
{
    public float3 target;
    public float speed;
    public bool navigateToTarget;

}
