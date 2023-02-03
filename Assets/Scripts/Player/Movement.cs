using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{ 
    public VariableJoystick joystick;
    public Animator animCtrl;
    public float Speed = 5f; 
    public float RotationSpeed = 10f;
    private NavMeshAgent agent;

    private void OnEnable()
    {
        TryGetComponent(out agent);
        if(agent)
            agent.isStopped = true;
    }

    protected virtual void MovementUpdate()
    {
        if (joystick == null)
            return;

        Vector2 direction = joystick.Direction;

        Vector3 movementVector = new (direction.x, 0, direction.y);

        transform.position = Vector3.Lerp(transform.position, transform.position + movementVector, Time.deltaTime * Speed);
        
        if (movementVector.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,  Quaternion.LookRotation(movementVector, Vector3.up), Time.deltaTime * RotationSpeed);
        }

        if (animCtrl == null)
            return;
        bool isWalking = direction.magnitude > 0;
        animCtrl.SetBool("IsWalking", isWalking);

        animCtrl.SetFloat("SpeedValue", direction.magnitude);
    }
    void Update()
    {
        MovementUpdate();
    }

}
