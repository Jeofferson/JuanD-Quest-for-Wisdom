using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{


    private const float LANE_DISTANCE = 3f;
    private const float TURN_SPEED = 7f;


    // Movement
    private int desiredLane = 1;    // 0 = Left, 1 = Middle, 2 = Right;
    private float jumpForce = 4f;
    private float gravity = 12f;
    private float verticalVelocity;
    private float speed = 7f;
    private CharacterController characterController;


    private void Start()
    {

        characterController = GetComponent<CharacterController>();
        
    }


    private void Update()
    {

        // Gather the inputs on which lane we should be
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            MoveLane(false);

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            MoveLane(true);

        }

        // Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;

        switch (desiredLane)
        {

            case 0:
                targetPosition += Vector3.left * LANE_DISTANCE;
                break;

            case 2:
                targetPosition += Vector3.right * LANE_DISTANCE;
                break;

        }

        // Calculate move delta
        Vector3 moveVector = Vector3.zero;

        #region Calculate X
        float tempNormalized = (targetPosition - transform.position).x;

        if (tempNormalized > 1)
        {

            tempNormalized = 1;

        }

        if (tempNormalized < -1)
        {

            tempNormalized = -1;

        }

        moveVector.x = tempNormalized * TURN_SPEED;
        #endregion

        #region Calculate Y
        moveVector.y = -0.1f;
        #endregion

        #region Calculate Z
        moveVector.z = speed;
        #endregion

        // Move the character
        characterController.Move(moveVector * Time.deltaTime);

    }


    private void MoveLane(bool isGoingRight)
    {

        desiredLane += isGoingRight ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

    }


}