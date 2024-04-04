using Unity.Entities;
using UnityEngine;

public class EntitySpawnerMono : MonoBehaviour
{
    public GameObject toSpawn;
    public float spawnDelay = 1f;
}

public class EntitySpawnerBaker : Baker<EntitySpawnerMono>
{
    public override void Bake(EntitySpawnerMono authoring)
    {
        var entitySpawnerEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entitySpawnerEntity, new EntitySpawnerData
        {
            toSpawn = GetEntity(authoring.toSpawn, TransformUsageFlags.Dynamic),
            spawnDelay = authoring.spawnDelay,
            timeLeft = authoring.spawnDelay
            
        });
    }
} 
