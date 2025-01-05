using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wander : State
{
    public float angleModifier = 1f;
    public bool isWandering = false;
    public float wanderingSpeed = 0.5f;
    public float rotationSpeed = 0.01f;
    private float movementSpeed = 0;
    Vector3? direction = null;
    private Animator animationController;
    private DinoBoundary dinoBoundary;
    private Vector3 boundaryCenter;

    void Start(){
        animationController = GetComponent<Animator>();
        GameObject boundaryObject = GameObject.Find("DinoBoundary");
        if (boundaryObject != null)
        {
            dinoBoundary = boundaryObject.GetComponent<DinoBoundary>();
            boundaryCenter = new Vector3 (248,27,250);
        }
        else
        {
            Debug.LogWarning("DinoBoundary not found!");
        }
    }

    private bool IsInBoundary()
    {
        return dinoBoundary.isCharacterInside(gameObject);
    }

    public override void Update()
    {
        base.Update();
        if (!IsInBoundary())
        {
            animationController.SetBool("isWalking", true);
            Vector3 directionToCenter = (boundaryCenter - gameObject.transform.position).normalized;
            movementController.Move(gameObject.transform.InverseTransformDirection(directionToCenter)*1f);
            return;
        }

        if (isWandering)
        {
            if (direction.HasValue)
            {
                movementController.Move(direction.Value.normalized * movementSpeed);
                bool isWalking = movementSpeed > 0 ? true:false;
                animationController.SetBool("isWalking", isWalking);
            }
            return;
        }
        isWandering = true;
        StartCoroutine(WanderAround());
    }

    IEnumerator WanderAround()
    {

            Vector3 rotationDirection = RotateAgent();
            direction = rotationDirection;
            movementSpeed = wanderingSpeed;
            yield return new WaitForSeconds(2);
            StartCoroutine(LookAround());
    }

    private Vector3 RotateAgent()
    {
            float wanderOrientation = Random.Range(-15f, 15f) * angleModifier;
            var newRotation = Quaternion.AngleAxis(wanderOrientation, Vector3.up);
            var rotationDirection = newRotation * Vector3.forward;
            movementController.Rotate(rotationDirection);
            movementSpeed = rotationSpeed;
            return rotationDirection;
    }

    IEnumerator LookAround()
    {
        direction = null;
        movementController.Move(Vector3.zero);
        Vector3 rotationDirection = RotateAgent();
        direction = rotationDirection;
        yield return new WaitForSeconds(3);
        isWandering = false;
    }
}