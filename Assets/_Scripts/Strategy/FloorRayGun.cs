using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FloorRayGun : MonoBehaviour
{
    public static FloorRayGun Instance = null;
    [HideInInspector]
    public UnityEvent<Vector3> RightClickedGround = new UnityEvent<Vector3>();

    [SerializeField]
    private LayerMask _layerMask;
    
    private Ray ray;
    

    [SerializeField]
    private InputAction _mouseClick;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _mouseClick.Enable();
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 999f);
    }

    void Update()
    {
        if (_mouseClick.WasPerformedThisFrame())
        {

            float mouseX = Mouse.current.position.x.ReadValue();
            float mouseY = Mouse.current.position.y.ReadValue();
            ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(mouseX, mouseY, 0f));
            var rayHit = Physics.Raycast(ray, out var raycastHit, 999f, _layerMask);
            if (rayHit)
            {
                RightClickedGround.Invoke(raycastHit.point);
            }
        }
    }
}
