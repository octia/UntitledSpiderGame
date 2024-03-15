using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FloorRayGun : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<Vector3> RightClickedGround;

    public static FloorRayGun Instance = null;
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

    // Update is called once per frame
    void Update()
    {
        if (_mouseClick.WasPerformedThisFrame())
        {
            Debug.Log("right click");

            float mouseX = Mouse.current.position.x.ReadValue();
            float mouseY = Mouse.current.position.y.ReadValue();
            ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(mouseX, mouseY, 0f));
            var rayHit = Physics.Raycast(ray, out var raycastHit, 999);
            if (rayHit)
            {
                Debug.Log("Ray hit");
                RightClickedGround.Invoke(raycastHit.point);
            }
        }
    }
}
