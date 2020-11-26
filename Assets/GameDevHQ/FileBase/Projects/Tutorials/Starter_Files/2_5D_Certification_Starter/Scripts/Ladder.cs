using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator on Ladder is Null");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetLadderClimb(true, transform.position, transform.localScale.y);
            _anim.SetBool("IsLadderClimbing", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogError("y scale = " + transform.localScale.y);
            other.GetComponent<Player>().SetLadderClimb(false, transform.position, transform.localScale.y);
            _anim.SetBool("IsLadderClimbing", false);
        }
    }
}
