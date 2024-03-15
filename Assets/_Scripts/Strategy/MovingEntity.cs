using DG.Tweening;
using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    private float _speed = 2f;
    private static Vector3 offset = Vector3.up;
    
    private void OnEnable()
    {
        FloorRayGun.Instance.RightClickedGround.AddListener(MoveToCoordinates);
    }

    public void MoveToCoordinates(Vector3 newPos)
    {
        Debug.Log("Moving");
        var currPos = transform.position;
        transform.DOMove(newPos + offset, Vector3.Distance(newPos, currPos) / _speed);
    }

    private void OnDisable()
    {
        FloorRayGun.Instance.RightClickedGround.RemoveListener(MoveToCoordinates);
    }

}
