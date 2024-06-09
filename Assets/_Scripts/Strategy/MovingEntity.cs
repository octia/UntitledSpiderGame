using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;


public class MovingEntity : MonoBehaviour
{

    [SerializeField]
    private NavMeshAgent _agent;
    
    [SerializeField]
    private float _speed = 2f;
    
    private void OnEnable()
    {
        //FloorRayGun.Instance.RightClickedGround.AddListener(MoveToCoordinates);
    }

    public void MoveToCoordinates(Vector3 newPos)
    {
        _agent.SetDestination(newPos);
        //var currPos = transform.position;
        //transform.DOMove(newPos, Vector3.Distance(newPos, currPos) / _speed);
    }

    private void OnDisable()
    {
        //FloorRayGun.Instance.RightClickedGround.RemoveListener(MoveToCoordinates);
    }

}
