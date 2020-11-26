using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 _startPos, _endPos;
    private float _movement = 120f;
    private bool _isRight = false;
    [SerializeField]
    private float _speed = 5f;
    private bool _isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + _movement);
    }

    private void FixedUpdate()
    {
        MovePlatform();
    }

    IEnumerator PlatformCoolDown()
    {
        _isMoving = false;
        yield return new WaitForSeconds(5f);
        _isRight = !_isRight;
        _isMoving = true;
    }

    private void MovePlatform()
    {
        float distance;
        if (_isMoving)
        {
            if (_isRight)
            {
                distance = _endPos.z - transform.position.z;
                if (Mathf.Abs(distance) < 0.1)
                {
                    transform.position = _endPos;
                    StartCoroutine(PlatformCoolDown());
                }
            }
            else
            {
                distance = _startPos.z - transform.position.z;
                if (Mathf.Abs(distance) < 0.1)
                {
                    transform.position = _startPos;
                    StartCoroutine(PlatformCoolDown());
                }

            }
            if (distance > 1)
            {
                distance = 1;
            }
            else if (distance < -1)
            {
                distance = -1;
            }
            Vector3 zDirection = new Vector3(0, 0, distance);
            transform.Translate(zDirection * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.parent == null)
            {
            other.transform.parent = transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
