﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerDoor : MonoBehaviour
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
            Debug.LogError("Lift on LowerDoor is Null");
        }

        _renderer = GetComponent<MeshRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Mesh Renderer on LowerDoor is Null");
        }

        if (_boxCollider == null)
        {
            Debug.LogError("Box Collider on LowerDoor is Null");
        }

        _liftDoor = GameObject.FindGameObjectWithTag("Lift_Door").GetComponent<LiftDoor>();
        if (_liftDoor == null)
        {
            Debug.LogError("Lift Door on LowerDoor is Null");
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
                if (_lift.GetFloorNumber() == 2)
                {
                    OpenDoor();
                    _liftDoor.OpenDoor();
                }
                else
                {
                    _lift.CallLift(2);
                }
            }
        }
    }
}
