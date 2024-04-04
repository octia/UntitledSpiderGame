using Unity.Entities;


public struct EntitySpawnerData : IComponentData
{
    public Entity toSpawn;
    public float spawnDelay;
    public float timeLeft;
}
