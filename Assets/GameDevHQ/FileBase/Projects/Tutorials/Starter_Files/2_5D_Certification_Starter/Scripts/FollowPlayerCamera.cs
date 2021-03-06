﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FollowPlayerCamera : MonoBehaviour
{
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
        if (_player == null)
        {
            Debug.LogError("Player on Follow Player Camera is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float z = _player.transform.position.z + 2f;
        float y = _player.transform.position.y + 4;
        this.transform.position = new Vector3(transform.position.x, y, z);
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
