using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody rb;

    #region Camera Variables
    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private GameObject playerCameraParent;

    [SerializeField]
    private float fov = 60f;
    public bool hasLookingRights = true;

    [SerializeField]
    private float mouseSensitivity = 2f;

    [SerializeField]
    private float maxLookAngle = 50f;
    float cameraRotation;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    #endregion

    #region CORE MOVEMENT
    [SerializeField]
    private bool hasMovingRights = true;
    public float walkSpeed = 5f;

    [SerializeField]
    private KeyCode sprintKey = KeyCode.LeftShift;

    [SerializeField]
    private float sprintCooldown = .5f;

    [SerializeField]
    private float airMultiplier = .75f;

    [SerializeField]
    private float groundDrag;

    private bool isWalking = false;
    private bool isSprinting = false;
    private float moveSpeed = 0.0f;

    private int speed = 0;

    public static playerMovement instance;

    #endregion

    #region SPRINTING

    [SerializeField]
    private bool hasSprintingRights = true;
    public float sprintSpeed = 11f;

    #endregion

    #region Jumping

    [SerializeField]
    private bool enableJump = true;

    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    public float jumpPower = 12f;

    [SerializeField]
    private float jumpCooldown = 1;

    public bool isGrounded = false;
    bool canJump = true;

    #endregion

    #region Animations

    [SerializeField]
    private GameObject Player;

    private Animator playerAnim;
    private bool isFalling = false;
    private bool isJumping = false;
    private bool isMoving = false;
    private float currentSpeed = 0f;

    #endregion

    private float horizontalInput;
    private float verticalInput;
    AudioSource walkSE;

    [SerializeField]
    private Animator animator;
    bool isPlaying = false;

    public void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        walkSE = GetComponent<AudioSource>();
        playerAnim = Player.GetComponent<Animator>();

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
        CheckGround();
        GetInput();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            hasLookingRights = !hasLookingRights;
        }
        if (hasLookingRights)
        {
            Cursor.lockState = CursorLockMode.Locked;
            // Minecraft type beat
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");

            // Limit pitch of camera
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCameraParent.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // limit movement speeds
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        // handle drag
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    public void FixedUpdate()
    {
        if (hasMovingRights)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(horizontalInput, 0, verticalInput);

            targetVelocity = transform.TransformDirection(targetVelocity);

            moveSpeed = (Input.GetKey(sprintKey) ? sprintSpeed : walkSpeed);

            currentSpeed = Mathf.Sqrt(
                Mathf.Pow(horizontalInput * moveSpeed, 2) + Mathf.Pow(verticalInput * moveSpeed, 2)
            );
            if (currentSpeed == 0)
                playerAnim.SetFloat("Speed", 0);
            else if (currentSpeed != 0 && currentSpeed < 15)
                playerAnim.SetFloat("Speed", 10);
            else if (currentSpeed != 0 && currentSpeed >= 15)
                playerAnim.SetFloat("Speed", 15);

            Debug.Log(playerAnim.GetFloat("Speed"));
            // on ground
            if (isGrounded)
            {
                animator.SetBool("isGrounded", true);
                animator.SetBool("isJumping", false);
                isJumping = false;
                animator.SetBool("isFalling", false);
                isFalling = false;

                rb.AddForce(targetVelocity.normalized * moveSpeed * 10f, ForceMode.Force);
                if (targetVelocity != Vector3.zero && isPlaying == false)
                {
                    isPlaying = true;
                    walkSE.Play();
                }
                else if (targetVelocity == Vector3.zero)
                {
                    isPlaying = false;
                    walkSE.Stop();
                }
            }
            // in air
            else if (!isGrounded)
            {
                animator.SetBool("isGrounded", false);
                if (rb.velocity.y < -0.1)
                {
                    animator.SetBool("isFalling", true);
                    isFalling = true;
                }

                rb.AddForce(
                    targetVelocity.normalized * moveSpeed * 10f * airMultiplier,
                    ForceMode.Force
                );
                isPlaying = false;
                walkSE.Stop();
                //isFalling = true;
                //playerAnim.SetBool("isFalling", isFalling);
            }
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // check for jumsp
        if (Input.GetKey(jumpKey) && canJump && isGrounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    // Sets isGrounded based on a raycast sent straigth down from the player object
    private void CheckGround()
    {
        Vector3 origin = new Vector3(
            transform.position.x,
            transform.position.y - (transform.localScale.y * .2f),
            transform.position.z
        );
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
            isGrounded = true;
            isFalling = false;
            playerAnim.SetBool("isFalling", isFalling);
        }
        else
        {
            isGrounded = false;
            isFalling = true;
            playerAnim.SetBool("isFalling", isFalling);
        }
        // Debug.Log(hit.collider);
        // Debug.DrawRay(origin, direction * distance, Color.red);
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        animator.SetBool("isJumping", true);
        isJumping = true;
    }

    private void ResetJump()
    {
        canJump = true;
    }
}
