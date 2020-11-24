using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private bool _moveDown;
    private Vector3 _originPos;
    private Vector3 _lowerPos;
    [SerializeField]
    private float _movement = 38f;
    [SerializeField]
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _originPos = transform.position;
        _lowerPos = new Vector3(transform.position.x, (transform.position.y - _movement), transform.position.z);        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_moveDown)
        {            
            if (transform.position.y <= _lowerPos.y)
            {

                transform.Translate(Vector3.zero);
            }
            else
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.y >= _originPos.y)
            {
                transform.Translate(Vector3.zero);
            }
            else
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }
        }
    }

    IEnumerator ElevatorCooldown()
    {
        yield return new WaitForSeconds(5f);
        _moveDown = !_moveDown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Lift OnTriggerEnter");
            StartCoroutine(ElevatorCooldown());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
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
