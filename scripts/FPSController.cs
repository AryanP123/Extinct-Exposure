using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private EncyclopediaMenu encyclopediaMenu;

    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    // public Vector3 startingPosition;
 
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
 
 
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
 
    public bool canMove = true;
    
    CharacterController characterController;

    public static bool isGameActive = false;

    private Animator animatorController;
    private AudioSource audioSource;
    public AudioClip footstepSound;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        LockCursor(false); // start with cursor unlocked to interact w/ menu
        // startingPosition = transform.position;  // Store the initial position of the player
        animatorController = GetComponent<Animator>(); // get animator for character
        audioSource = GetComponent<AudioSource>(); // get audio source for character
    }
 
    void Update()
    {
        // if (!isGameActive) { return; }   

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed!");
            /* if (pauseMenu.isPaused) {
                pauseMenu.ResumeGame();
            }
            else { */
                pauseMenu.PauseGame();
            //}
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (encyclopediaMenu.isEncyclopediaOpen) {
                encyclopediaMenu.CloseEncyclopedia();
            }
            else {
                encyclopediaMenu.OpenEncyclopedia();
            }
        }

         // Disable movement while paused
        if (!canMove || !isGameActive) return;

        #region Handles Movment
        // Starts animation upon movement
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow);
        animatorController.SetBool("isWalkingChar", isWalking);

        if(isWalking && !audioSource.isPlaying){
            audioSource.clip = footstepSound;
            audioSource.Play();
        }
        else if(!isWalking && audioSource.isPlaying){
            audioSource.Stop();
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
 
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
 
        #endregion
 
        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
 
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
 
        #endregion
 
        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);
 
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
 
        #endregion
    }

    // helper to lock or unlock cursor
    public void LockCursor(bool lockCursor)
    {
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

    /* public void ResetPosition()
    {
        transform.position = startingPosition; // Reset the player's position to the initial one
    } */
}