using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb;

    #region CAMERA VARIABLES
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float fov = 60f;
    [SerializeField] private bool invertCamera = false;
    [SerializeField] private bool cameraCanMove = true;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 50f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    #endregion
    
    #region CORE MOVEMENT
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private bool playerCanMove = true;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float maxVelocityChange = 10f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private float sprintCooldown = .5f;

    private bool isWalking = false;

    #endregion

    #region CROUCHING

    [SerializeField] private bool enableCrouch = true;
    [SerializeField] private bool holdToCrouch = true;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private float crouchHeight = .75f;
    [SerializeField] private float speedReduction = .5f;
    
    private bool isCrouched = false;
    private Vector3 originalScale;

    #endregion

    #region JUMPING

    [SerializeField] private bool enableJump = true;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float jumpPower = 5f;

    private bool isGrounded = false;

    #endregion
    
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // TODO: Add crosshair later
        // Set internal variables
        playerCamera.fieldOfView = fov;
        originalScale = transform.localScale;
    }
    
    public void Update()
    {
        
    }

}