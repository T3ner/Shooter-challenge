using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    [SerializeField] Animator handAnim;
    CharMovement move;

    private void Awake()
    {
        move = GetComponentInParent<CharMovement>();
    }
    void Update()
    {
        if (!move.grounded)
        {
            handAnim.Play("Jump");
        }

        if (Input.GetMouseButtonDown(0))
        {
            handAnim.Play("Punch");
        }
    }
}
