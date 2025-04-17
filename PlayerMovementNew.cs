using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementNew : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform camera;
    [SerializeField] private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float sprintTransitSpeed = 5f;
    [SerializeField] private float turningSpeed = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f;

    private float VerticalVelocity;
    private float speed;

    [Header("Input")]
    private float moveInput;
    private float turnInput;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        InputManagement();
        Movement();

        float animationSpeed = new Vector2(turnInput, moveInput).sqrMagnitude;
        animator.SetFloat("Speed", animationSpeed);
        //Debug.Log(animationSpeed);

    }
    private void GroundMovement()
    {
        //Vector3 move = new Vector3(turnInput, 0 , moveInput);
        //move = transform.TransformDirection(move) * speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
        }

        Vector3 move = new Vector3(turnInput, 0 , moveInput);
        move = transform.TransformDirection(move) * speed;

        move.y = VerticalForceCalculation();

        controller.Move(move * Time.deltaTime);
    }
    private void Turn()
    {
        if(Mathf.Abs(turnInput) < 0 || Mathf.Abs(moveInput) > 0)
        {
        Vector3 currentLookDirection = camera.forward;
        currentLookDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);
        }
    }
    private float VerticalForceCalculation()
    {
        if (controller.isGrounded)
        {
            VerticalVelocity = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                VerticalVelocity = Mathf.Sqrt(jumpHeight * 2 * gravity);
            }
        }
        else
        {
            VerticalVelocity -= gravity * Time.deltaTime;
        }
        return VerticalVelocity;
    }
    private void Movement()
    {
        GroundMovement();
        Turn();
    }
    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
}
