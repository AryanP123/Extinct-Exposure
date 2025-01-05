using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class UniversalMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float rotationTime = 0.5f;
    public float gravity = 30f;


    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    public void Update(){
        
        if (!controller.isGrounded)
            {
                controller.Move(new Vector3(0, -gravity * Time.deltaTime*50f,0));
            }
    }
    public virtual void Move(Vector3 globalDirection)
    {
        if (globalDirection.Equals(Vector3.zero))
        {
            return;
        }
        Vector3 direction = transform.right * globalDirection.x + transform.forward * globalDirection.z;
        
        controller.Move(direction * speed * Time.deltaTime);
        if (globalDirection.z == -1 && globalDirection.x == 0)
            return;
        Rotate(direction.normalized);

    }

    public virtual void Rotate(Vector3 direction)
    {
        if (direction.magnitude != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), rotationTime);
        };
    }
}
