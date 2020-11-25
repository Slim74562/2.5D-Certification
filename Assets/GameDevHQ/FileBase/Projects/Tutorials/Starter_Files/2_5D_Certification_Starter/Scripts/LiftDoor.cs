using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoor : MonoBehaviour
{
    [SerializeField]
    BoxCollider _boxCollider;
    [SerializeField]
    MeshRenderer _renderer;

    private void Start()
    {  
        if (_boxCollider == null)
        {
            _boxCollider = GetComponent<BoxCollider>();
            if (_boxCollider == null)
            {
                Debug.LogError("Box Collider is Null");
            }
        }

        if (_renderer == null)
        {
            _renderer = GetComponent<MeshRenderer>();
            if (_renderer == null)
            {
                Debug.LogError("Mesh Renderer is Null");
            }
        }
    }

    public void CloseDoor()
    {
        _boxCollider.enabled = true;
        _renderer.enabled = true;
    }

    public void OpenDoor()
    {

        _boxCollider.enabled = false;
        _renderer.enabled = false;
    }   
}
