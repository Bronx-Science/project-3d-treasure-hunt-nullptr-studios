using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb;

    #region CAMERA VARIABLES
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject playerCameraParent;
    [SerializeField] private float fov = 60f;
    [SerializeField] private bool hasLookingRights = true;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 50f;
    float cameraRotation;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    #endregion
    
    #region CORE MOVEMENT
    [SerializeField] private bool hasMovingRights = true;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private float sprintCooldown = .5f;
    [SerializeField] private float airMultiplier = .75f;
    [SerializeField] private float groundDrag;

    private bool isWalking = false;
    private bool isSprinting = false;

    #endregion

    #region SPRINTING

    [SerializeField] private bool hasSprintingRights = true;
    [SerializeField] private float sprintSpeed = 11f;


    #endregion

    #region JUMPING

    [SerializeField] private bool enableJump = true;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float jumpPower = 12f;
    [SerializeField] private float jumpCooldown = 1;

    private bool isGrounded = false;
    bool canJump = true;

    #endregion
    
    float horizontalInput;
    float verticalInput;
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Set internal variables
        playerCamera.fieldOfView = fov;

    }
    
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // TODO: Add crosshair later
        // Set internal variables
        playerCamera.fieldOfView = fov;
    }

    
    public void Update()
    {
        if(hasLookingRights)
        {
            // Minecraft type beat
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");

            // Limit pitch of camera
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCameraParent.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }

        // handle drag
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    public void FixedUpdate()
    {
        GetInput();

        if (hasMovingRights)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(horizontalInput, 0, verticalInput);

            targetVelocity = transform.TransformDirection(targetVelocity);

            var moveSpeed = (Input.GetKey(sprintKey) ? sprintSpeed : walkSpeed);
            // on ground
            if(isGrounded)
                rb.AddForce(targetVelocity.normalized * moveSpeed * 10f, ForceMode.Force);
            // in air
            else if(!isGrounded)
                rb.AddForce(targetVelocity.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            // limit movement speeds
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if(flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        if(enableJump && Input.GetKey(jumpKey) && isGrounded)
        {
            Jump();
        }

        CheckGround();
    }

     private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && canJump && isGrounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

// Sets isGrounded based on a raycast sent straigth down from the player object
    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

   private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        canJump = true;
    }

}