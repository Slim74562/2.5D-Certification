using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleDoor : MonoBehaviour
{
    private Lift _lift;
    private MeshRenderer _renderer;
    [SerializeField]
    private BoxCollider _boxCollider;
    private LiftDoor _liftDoor;

    private void Start()
    {
        _lift = GameObject.FindGameObjectWithTag("Lift_Floor").GetComponent<Lift>();
        if (_lift == null)
        {
            Debug.LogError("Lift on MiddleDoor is Null");
        }

        _renderer = GetComponent<MeshRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Mesh Renderer on MiddleDoor is Null");
        }

        if (_boxCollider == null)
        {
            Debug.LogError("Box Collider on MiddleDoor is Null");
        }

        _liftDoor = GameObject.FindGameObjectWithTag("Lift_Door").GetComponent<LiftDoor>();
        if(_liftDoor == null)
        {
            Debug.LogError("Lift Door on MiddleDoor is Null");
        }

    }

    public void CloseDoor()
    {
        _renderer.enabled = true;
        _boxCollider.enabled = true;
    }


    public void OpenDoor()
    {
        _renderer.enabled = false;
        _boxCollider.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_lift.GetFloorNumber() == 1)
                {
                    OpenDoor();
                    _liftDoor.OpenDoor();
                }
                else
                {
                    _lift.CallLift(1);
                }
            }
        }
    }
}
