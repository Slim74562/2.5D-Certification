using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 9.0f;
    private float _gravity = 30.0f;
    private Vector3 _direction;
    private float _jumpHeight = 20f;
    private CharacterController _controller;
    private Animator _anim;
    private bool _jumping;

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
        if (_controller.isGrounded)
        {
            float horizontal = Input.GetAxisRaw("Horizontal"); // get axis raw will make it automatically go to 1, -1, or 0 right away instead of gradually going to them
            _anim.SetFloat("Speed", Mathf.Abs(horizontal));

            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("IsJumping", _jumping);
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
                            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumping = true;
                _anim.SetBool("IsJumping", _jumping);
                _direction.y += _jumpHeight;                
            }
        }
        else
        {
            _direction.y -= _gravity * Time.deltaTime;
        }
        _controller.Move(_direction * Time.deltaTime);
    }
}
