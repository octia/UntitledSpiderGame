using UnityEngine;

// Quick and dirty. Will rework when needed
public class ColorRandomiser : MonoBehaviour
{
    private MeshRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();

        if (_renderer)
        {
            _renderer.material.color = Random.ColorHSV();
        }
    }

    

}
