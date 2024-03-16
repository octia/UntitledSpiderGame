using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private float _spawnCircleRadius = 4f;

    #region InputActions
    [SerializeField]
    private InputAction _spawnEntity;

    [SerializeField]
    private InputAction _removeEntity;
    #endregion

    private List<GameObject> _spawned = new List<GameObject>();

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
                var spawnPos = ((Vector3)Random.insideUnitCircle.normalized*_spawnCircleRadius).SwapYZ().WithY(1);
                var newSpawn = Instantiate(_prefab, spawnPos, Quaternion.identity, transform);
                _spawned.Add(newSpawn);
            }
            if (_removeEntity.WasPerformedThisFrame())
            {
                var toRemovei = _spawned.Count - 1;
                var toRemove = _spawned[toRemovei];
                Destroy(toRemove);
                _spawned.RemoveAt(toRemovei);
            }
        }
    }
}
