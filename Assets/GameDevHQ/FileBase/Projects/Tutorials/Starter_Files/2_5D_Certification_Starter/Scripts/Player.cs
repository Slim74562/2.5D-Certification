using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float _speed = 9.0f;
    private float _gravity = 30.0f;
    private Vector3 _direction;
    private float _jumpHeight = 16f;
    private CharacterController _controller;
    private Animator _anim;
    private bool _isJumping;
    private bool _isHanging;
    private bool _isClimbing;
    private bool _isRolling;
    [SerializeField]
    private bool _isLadderClimbing;
    [SerializeField]
    private float _climbSpeed = 3.0f;
    private float _originH;
    [SerializeField]
    private bool _climbUp;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Character Controller on Player is Null");
        }

        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator on Player is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ClimbLedge();
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.R) || transform.position.y < -50)
        {
            SceneManager.LoadScene(0);
        }
    }

    void CalculateMovement()
    {
        if (_controller.isGrounded)
        {

            float horizontal = Input.GetAxisRaw("Horizontal"); // get axis raw will make it automatically go to 1, -1, or 0 right away instead of gradually going to them
            _anim.SetFloat("Speed", Mathf.Abs(horizontal));

            if (_isJumping)
            {
                _isJumping = false;
                _anim.SetBool("IsJumping", _isJumping);
            }
            if (_isRolling)
            {
                _isRolling = false;
                _anim.SetBool("IsRolling", _isRolling);
            }
            if (!_isLadderClimbing)
            {
                _originH = horizontal;
            }

            _direction = new Vector3(0, 0, horizontal) * _speed;

            if (horizontal != 0 && !_isLadderClimbing)
            {
                Vector3 facingDirection = transform.localEulerAngles;
                facingDirection.x = 0;
                facingDirection.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facingDirection;
            }

            if (Input.GetKey(KeyCode.Space))
            {

                _direction.y += _jumpHeight;
                _isJumping = true;
                _anim.SetBool("IsJumping", _isJumping);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _isRolling = true;
                _anim.SetBool("IsRolling", _isRolling);
                _controller.enabled = false;
            }
        }
        else
        {
            _direction.y -= _gravity * Time.deltaTime;
        }

        if (_controller.enabled == true && !_isLadderClimbing)
        {            
            _controller.Move(_direction * Time.deltaTime);
        }
        if (_isLadderClimbing)
        {
            Vector3 ladderAngle = transform.localEulerAngles, movement;
            ladderAngle.x = 30;
            
            if (_climbUp)
            {
                movement = new Vector3(0, 1.25f, _originH);
                _controller.Move(movement * _climbSpeed * Time.deltaTime);
                ladderAngle.y = 180;
            }
            else
            {
                movement = new Vector3(0, -1.5f, -_originH/5);
                transform.Translate(movement * _climbSpeed * Time.deltaTime);
                ladderAngle.y = 180;
            }
            transform.localEulerAngles = ladderAngle;
        }
    }

    public void SetLadderClimb(bool isLadderClimbing, Vector3 ladderPos, float ySize)
    {
        
        _isLadderClimbing = isLadderClimbing;    
        if (_isLadderClimbing)
        {
            if (ladderPos.y + (ySize / 2) >= transform.position.y)
            {
                _climbUp = true;
            }
            else
            {
                _climbUp = false;
            }
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(10f);
        _speed /= 2;
    }

    public void SpeedPowerup()
    {
        _speed *= 3;
        StartCoroutine(SpeedPowerDown());
    }

    public void RollReset()
    {
        _isRolling = false;
        _anim.SetBool("IsRolling", _isRolling);
        _controller.enabled = true;
    }

    private void ClimbLedge()
    {
        if (_isHanging && Input.GetKeyDown(KeyCode.E))
        {
            _isClimbing = true;
            _anim.SetBool("IsClimbing", _isClimbing);
            _isHanging = false;
        }
    }

    public void GrabLedge(float hangDistance)
    {
        _controller.enabled = false;
        _isHanging = true;
        _anim.SetBool("IsHanging", _isHanging);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + hangDistance);
    }

    public void LedgeClimbComplete()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 4.68417f, transform.position.z + 1.2906f);
        _isHanging = false;
        _isClimbing = false;
        _isJumping = false;
        _anim.SetBool("IsJumping", _isJumping);
        _anim.SetBool("IsHanging", _isHanging);
        _anim.SetBool("IsClimbing", _isClimbing);
        _controller.enabled = true;
    }
}
