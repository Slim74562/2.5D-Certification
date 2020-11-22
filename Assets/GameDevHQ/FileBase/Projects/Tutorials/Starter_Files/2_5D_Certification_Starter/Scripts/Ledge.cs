using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private float _hangDistance = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_Grab_Checker"))
        {
            Player player = other.GetComponentInParent<Player>();

            if (player != null)
            {
                player.GrabLedge(_hangDistance);
            }
        }
    }
}
