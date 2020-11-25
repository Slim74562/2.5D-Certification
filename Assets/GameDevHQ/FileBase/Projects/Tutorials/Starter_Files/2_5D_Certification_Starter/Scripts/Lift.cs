using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private bool _moveDown;
    private bool _isMoving;
    private bool _isUp;
    private Vector3 _originPos;
    private Vector3 _lowerPos;
    [SerializeField]
    private float _movement = 38f;
    [SerializeField]
    private float _speed;
    LiftDoor _liftDoor;
    LowerDoor _lowerDoor;
    UpperDoor _upperDoor;

    // Start is called before the first frame update
    void Start()
    {
        _originPos = transform.position;
        _lowerPos = new Vector3(_originPos.x, (_originPos.y - _movement), _originPos.z);
        _liftDoor = GetComponentInChildren<LiftDoor>();
        _lowerDoor = GameObject.Find("Lower_Door").GetComponent<LowerDoor>();
        _upperDoor = GameObject.Find("Upper_Door").GetComponent<UpperDoor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_moveDown)
        {
            if (transform.position.y <= _lowerPos.y)
            {
                transform.position = _lowerPos;
                _liftDoor.OpenDoor();
                _lowerDoor.OpenDoor();
                if (_isMoving)
                {
                    _isMoving = false;
                    _isUp = false;
                }
            }
            else
            {
                _upperDoor.CloseDoor();
                _isMoving = true;
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
                _liftDoor.CloseDoor();
            }
        }
        else
        {
            if (transform.position.y >= _originPos.y)
            {
                transform.position = _originPos;
                _liftDoor.OpenDoor();
                _upperDoor.OpenDoor();
                if (_isMoving)
                {
                    _isMoving = false;
                    _isUp = true;
                }
            }
            else
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
                _liftDoor.CloseDoor();
                _isMoving = true;
                _lowerDoor.CloseDoor();
            }
        }
    }

    IEnumerator ElevatorCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        _moveDown = !_moveDown;
        yield return new WaitForSeconds(0.5f);
        _isMoving = true;

    }

    public bool IsLiftUp()
    {
        return _isUp;
    }

    public void CallLift()
    {
        if (!_isMoving)
        {
            StartCoroutine(ElevatorCooldown());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CallLift();
            }
            if (other.transform.parent == null)
            {
                other.transform.parent = this.transform;
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
