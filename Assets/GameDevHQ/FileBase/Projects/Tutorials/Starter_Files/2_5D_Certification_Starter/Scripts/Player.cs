using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            _direction = new Vector3(0, 0, horizontal) * _speed;

            /* Character flip can also be done like this
             * if (horizontal != 0)
             * {
             * Vector3 facingDirection = transform.localEulerAngles;
             * facingDirection.y = _direction.z > 0 ? 0 : 180;
             * transform.localEulerAngles = facingDirection;
             * }
             */

            if (horizontal > 0)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
            }
            else if (horizontal < 0)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.back);
            }

            if (Input.GetKey(KeyCode.Space))
            {

                _direction.y += _jumpHeight;
                _isJumping = true;
                _anim.SetBool("IsJumping", _isJumping);
            }            
        }
        else
        {
            _direction.y -= _gravity * Time.deltaTime;
        }
        _controller.Move(_direction * Time.deltaTime);
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
