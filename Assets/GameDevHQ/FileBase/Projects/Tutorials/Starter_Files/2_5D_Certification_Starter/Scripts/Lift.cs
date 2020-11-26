using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private bool _isMoving = false;
    private Vector3 _originPos;
    private Vector3 _lowerPos;
    private Vector3 _middlePos;
    [SerializeField]
    private float _movement = 38f;
    [SerializeField]
    private float _middleMove = 15f;
    [SerializeField]
    private float _speed;
    LiftDoor _liftDoor;
    LowerDoor _lowerDoor;
    UpperDoor _upperDoor;
    MiddleDoor _middleDoor;
    [SerializeField]
    private int _floor = 1;
    private GameObject _rightPanel;
    private bool _isReady = false;

    // Start is called before the first frame update
    void Start()
    {
        _originPos = transform.position;
        _lowerPos = new Vector3(_originPos.x, (_originPos.y - _movement), _originPos.z);
        _middlePos = new Vector3(_originPos.x, (_originPos.y - _middleMove), _originPos.z);
        
        _liftDoor = GetComponentInChildren<LiftDoor>();
        _lowerDoor = GameObject.Find("Lower_Door").GetComponent<LowerDoor>();
        _upperDoor = GameObject.Find("Upper_Door").GetComponent<UpperDoor>();
        _middleDoor = GameObject.Find("Middle_Door").GetComponent<MiddleDoor>();
        _rightPanel = GameObject.Find("Scifi_Floors_01");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 floorStop;
        float yDifference = 0;
        if (_floor == 0 && _isMoving)
        {
            yDifference = _originPos.y - transform.position.y;
            if (Mathf.Abs(yDifference) < 0.01)
            {
                transform.position = _originPos;
            }
            if (transform.position.y == _originPos.y)
            {               
                _liftDoor.OpenDoor();
                _upperDoor.OpenDoor();
                _isMoving = false;
                _isReady = true;
            }
            else
            {
                _liftDoor.CloseDoor();
                _lowerDoor.CloseDoor();
                _upperDoor.CloseDoor();
                _middleDoor.CloseDoor();
            }
        }
        else if (_floor == 1 && _isMoving)
        {
            yDifference = _middlePos.y - transform.position.y;
            if (Mathf.Abs(yDifference) < 0.1)
            {
                transform.position = _middlePos;
            }
            if (transform.position.y == _middlePos.y)
            {
                _middleDoor.OpenDoor();
                _rightPanel.SetActive(false);
                _isMoving = false;
                _isReady = true;
            }
            else
            {
                _upperDoor.CloseDoor();
                _lowerDoor.CloseDoor();
                _middleDoor.CloseDoor();
                _liftDoor.CloseDoor();
            }
        }
        else if (_floor == 2 && _isMoving)
        {
            yDifference = _lowerPos.y - transform.position.y;
            if (Mathf.Abs(yDifference) < 0.1)
            {
                transform.position = _lowerPos;
            }
            if (transform.position.y == _lowerPos.y)
            {
                _liftDoor.OpenDoor();
                _lowerDoor.OpenDoor();
                _isMoving = false;
                _isReady = true;
            }
            else
            {
                _rightPanel.SetActive(true);
                _upperDoor.CloseDoor();
                _middleDoor.CloseDoor();
                _lowerDoor.CloseDoor();
                _liftDoor.CloseDoor();
            }
        }
        if (Mathf.Abs(yDifference) > 1)
        {
            if (yDifference < -1)
            {
                yDifference = -1;
            }
            else
            {
                yDifference = 1;
            }
        }
        floorStop = new Vector3(0, yDifference, 0);
        transform.Translate(floorStop * _speed * Time.deltaTime);
    }

    IEnumerator ElevatorCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        _isMoving = true;
    }

    public int GetFloorNumber()
    {
        return _floor;
    }

    public void CallLift(int floor)
    {
        _floor = floor;
        if (!_isMoving)
        {
            StartCoroutine(ElevatorCooldown());
        }
    }
    
    private void CallLift()
    {
        if (!_isMoving)
        {
            if (_floor + 1 > 2)
            {
                _floor = 0;
            }
            else
            {
                _floor++;
            }
        }
        CallLift(_floor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isReady = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E) && _isReady)
            {
                _isReady = false;
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
