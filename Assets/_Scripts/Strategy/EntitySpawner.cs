using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    private List<GameObject> _spawned = new List<GameObject>();

    [SerializeField]
    private InputAction _spawnEntity;

    [SerializeField]
    private InputAction _removeEntity;
    

    private void Awake()
    {
        _spawnEntity.Enable();
        _removeEntity.Enable();
    }


    // Update is called once per frame
    void Update()
    {
        if (_prefab)
        {
            if (_spawnEntity.WasPerformedThisFrame())
            {
                var newSpawn = Instantiate(_prefab, ((Vector3)Random.insideUnitCircle.normalized*10).SwapYZ().WithY(1), Quaternion.identity, transform);
                _spawned.Add(newSpawn);
            }
            if (_removeEntity.WasPerformedThisFrame())
            {
                var toRemovei = _spawned.Count;
                var toRemove = _spawned[toRemovei];
                Destroy(toRemove);
                _spawned.RemoveAt(toRemovei);
            }
        }
    }
}
