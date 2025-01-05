using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoMovement : UniversalMovement
{
    Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void Move(Vector3 globalDirection)
    {
        if (globalDirection.magnitude != 0)
            animator.SetFloat("velocity", 1);
        else
            animator.SetFloat("velocity", 0);
        base.Move(globalDirection);
    }
}
