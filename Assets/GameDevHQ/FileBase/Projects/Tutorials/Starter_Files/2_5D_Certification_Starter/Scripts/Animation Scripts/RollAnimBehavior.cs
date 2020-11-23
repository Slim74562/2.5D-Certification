using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAnimBehavior : StateMachineBehaviour
{
    [SerializeField]
    float _movePlayerY = 1;
    Transform _playerTransform;
    [SerializeField]
    private float _speed = 0.25f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerTransform = animator.gameObject.transform.parent.GetComponent<Transform>();
        if (_playerTransform != null)
        {
            _playerTransform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y - _movePlayerY, _playerTransform.position.z);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerTransform != null)
        {
            Vector3 newPos = new Vector3(0, 0, 1f);
            _playerTransform.Translate(newPos * _speed * Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerTransform != null)
        {
            _playerTransform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y + _movePlayerY, _playerTransform.position.z);
        }
        Player player = animator.gameObject.transform.parent.GetComponent<Player>();
        if (player != null)
        {
            player.RollReset();
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
