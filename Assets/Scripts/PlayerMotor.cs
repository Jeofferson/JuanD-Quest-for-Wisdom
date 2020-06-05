using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{


    private const float LANE_DISTANCE = 3f;

    private const float ORIGINAL_SPEED = 10f;
    private const float TURN_SPEED = ORIGINAL_SPEED * 2f;
    private const float JUMP_FORCE = 20f;

    // Movement X
    private int desiredLane = 1;    // 0 = Left, 1 = Middle, 2 = Right;

    // Movement Y
    private float gravity = 12f;

    private float fallForce = JUMP_FORCE / 4f;
    private float verticalVelocity;

    // Movement Z
    private float speed = ORIGINAL_SPEED;

    // Player
    private CharacterController characterController;

    // Animation
    private Animator animator;


    private void Start()
    {

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

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
        bool isGrounded = IsGrounded();

        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded)
        {

            verticalVelocity = -0.1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {

                Jump();

            }

        }
        else
        {

            verticalVelocity -= gravity * (Time.deltaTime * fallForce);

            // Fast falling mechanic
            if (Input.GetKeyDown(KeyCode.Space))
            {

                verticalVelocity = -JUMP_FORCE;

            }

        }

        moveVector.y = verticalVelocity;
        #endregion

        #region Calculate Z
        moveVector.z = speed;
        #endregion

        // Move the player
        characterController.Move(moveVector * Time.deltaTime);

        // Face the lane that the player will switch into
        Vector3 direction = characterController.velocity;
        direction.y = 0;

        if (direction != Vector3.zero)
        {

            transform.forward = Vector3.Lerp(transform.forward, direction, TURN_SPEED);

        }

    }


    private void MoveLane(bool isGoingRight)
    {

        desiredLane += isGoingRight ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

    }


    private void Jump()
    {

        animator.SetTrigger("Jump");
        verticalVelocity = JUMP_FORCE;

    }



    private bool IsGrounded()
    {

        Ray groundRay = new Ray(
            new Vector3(
                characterController.bounds.center.x,
                (characterController.bounds.center.y - characterController.bounds.extents.y) + 0.2f,
                characterController.bounds.center.z
            ),
            Vector3.down
        );

        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.red, 5.0f);

        return Physics.Raycast(groundRay, 0.2f + 0.1f);

    }


}