using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{


    private const float LANE_DISTANCE = 3.4f;

    private const float CHARACTER_CONTROLLER_ORIGINAL_CENTER_Y = .9f;
    private const float CHARACTER_CONTROLLER_ORIGINAL_HEIGHT = 1.8f;

    private const float ORIGINAL_SPEED = 10f;
    private readonly List<float> SPEED_INCREASE_INTERVALS = new List<float> { 3f, 3f };
    private readonly List<float> SPEED_INCREASE_MULTIPLIERS = new List<float> { 1.5f, 2f };
    private const float TURN_SPEED = ORIGINAL_SPEED * 2f;
    private const float JUMP_FORCE = 40f;
    private const float DELAY_BEFORE_JUMPING_AGAIN = 0.33f;
    private const float SLIDE_DURATION = 1.167f;

    private bool hasStartedRunning;
    private bool didDie;

    // Movement X
    private int desiredLane = 1;    // 0 = Left, 1 = Middle, 2 = Right;

    // Movement Y
    private float gravity = 12f;

    private float timeOfJump;
    private float fallForce = JUMP_FORCE / 4f;
    private float verticalVelocity;
    private bool jumpAgainImmediately;

    private bool isSliding;

    // Movement Z
    private float currentSpeed = ORIGINAL_SPEED;

    private Vector3 targetPosition;
    private Vector3 moveVector;

    private CharacterController characterController;
    private CameraMotor cameraMotor;

    private StatsManager statsManager;
    private DeathMenu deathMenu;

    private Animator animator;


    private void Start()
    {

        characterController = GetComponent<CharacterController>();
        cameraMotor = FindObjectOfType<CameraMotor>();

        statsManager = FindObjectOfType<StatsManager>();
        deathMenu = FindObjectOfType<DeathMenu>();

        animator = GetComponent<Animator>();

    }


    private void Update()
    {

        if (!hasStartedRunning) { return; }

        if (didDie)
        {

            //CalculateY();
            return;

        }

        // Identify which lane (swiping)
        if (MobileInput.Instance.SwipeLeft)
        {

            MoveLane(false);

        }

        if (MobileInput.Instance.SwipeRight)
        {

            MoveLane(true);

        }

        // Identify which lane (using keyboard)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            MoveLane(false);

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            MoveLane(true);

        }

        // Determine target  position
        targetPosition = transform.position.z * Vector3.forward;

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
        moveVector = Vector3.zero;

        CalculateX();
        CalculateY();
        CalculateZ();

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


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        switch (hit.gameObject.tag)
        {

            case "Obstacle":
                Die();
                break;

        }

    }


    public void StartRunning()
    {

        hasStartedRunning = true;
        animator.SetTrigger("StartRunning");

    }


    private void MoveLane(bool isGoingRight)
    {

        desiredLane += isGoingRight ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

    }


    private void CalculateX()
    {

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

    }


    private void CalculateY()
    {

        bool isGrounded = IsGrounded();

        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded)
        {

            verticalVelocity = -0.1f;

            if (MobileInput.Instance.SwipeUp)
            {

                Jump();
                timeOfJump = Time.time;

            }
            else if (MobileInput.Instance.SwipeDown)
            {

                if (!isSliding)
                {

                    Slide();

                }

            }
            else if (jumpAgainImmediately)
            {

                jumpAgainImmediately = false;

                Jump();
                timeOfJump = Time.time;

            }

        }
        else
        {

            verticalVelocity -= gravity * (Time.deltaTime * fallForce);

            // Fast fall
            if (MobileInput.Instance.SwipeDown)
            {

                jumpAgainImmediately = false;

                verticalVelocity = -JUMP_FORCE;

            }
            else if (MobileInput.Instance.SwipeUp && Time.time > timeOfJump + DELAY_BEFORE_JUMPING_AGAIN)
            {

                jumpAgainImmediately = true;

            }

        }

        moveVector.y = verticalVelocity;

    }


    private void CalculateZ()
    {

        moveVector.z = currentSpeed;

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


    private void Jump()
    {

        animator.SetTrigger("Jump");
        verticalVelocity = JUMP_FORCE;

    }


    private void Slide()
    {

        animator.SetTrigger("Slide");
        ShrinkCharacterController();

    }


    private void ShrinkCharacterController()
    {

        isSliding = true;

        characterController.center = new Vector3(characterController.center.x, characterController.center.y / 2, characterController.center.z);
        characterController.height /= 2;

        Invoke("RevertCharacterController", SLIDE_DURATION);

    }


    private void RevertCharacterController()
    {

        characterController.center = new Vector3(characterController.center.x, CHARACTER_CONTROLLER_ORIGINAL_CENTER_Y, characterController.center.z);
        characterController.height = CHARACTER_CONTROLLER_ORIGINAL_HEIGHT;

        isSliding = false;

    }


    public void Die()
    {

        didDie = true;

        cameraMotor.Die();
        statsManager.Die();
        deathMenu.Die();

        animator.SetTrigger("Die");

    }


}