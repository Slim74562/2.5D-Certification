﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_Grab_Checker"))
        {
            Player player = other.GetComponentInParent<Player>();

            if (player != null)
            {
                player.GrabLedge();
            }
        }
    }
}
