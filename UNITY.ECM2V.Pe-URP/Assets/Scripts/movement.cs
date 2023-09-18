using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public LayerMask restartCondition;
    public bool restart;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public Vector3 moveDirection;
    public Vector3 startPosition;

    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource loseSound;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startPosition = transform.position;

        //SetSpawnPosition();

    }

    void SetSpawnPosition()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Scene1")
        {
            // set the spawn position {
            Vector3 spawnPosition = new Vector3(41f, 60f, 0f);

            // set the player's position to the spawn position
            transform.position = spawnPosition;
        }

        else
        {
            Vector3 spawnPosition = new Vector3(0f, 188f, -9f);

            // set the player's position to the spawn position
            transform.position = spawnPosition;
        }




    }



    void Update()
    {
        //shoots a raycast to check if player is grounded or not 
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        restart = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, restartCondition);

        if (grounded || restart)
            rb.drag = groundDrag;

        else
            rb.drag = 0;

        if (Input.GetKeyDown(jumpKey))
        {
            Debug.Log("Spacebar Pressed");
        }

        MyInput();
    }

    void OnCollisionEnter(Collision collision)
    {
        //restart level
        if (collision.gameObject.layer == LayerMask.NameToLayer("restartCondition"))
        {
            Debug.Log("Collided");

            loseSound.Play();

            SetSpawnPosition();


        }

    }


    void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        //handles movement and jumping
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

        }
    }

    void MovePlayer()
    {
        // applies force to rb 
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    void Jump()
    {
        //applies an upword force to rb
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        jumpSound.Play();
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}


/*using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class movement : MonoBehaviour
{
    public float speed = 5f;
    public float sprint = 8.5f;
    public float jumpSpeed = 7.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90;

    public Transform playerCameraPivot;

    CharacterController characterController;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
      
        if (playerCameraPivot == null)
        {
            Debug.LogError("PlayerCameraPivot not assigned!");
        }


    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * 0.7f * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canMove)
            {
                speed = sprint;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) && canMove)
            {
                speed = 5f;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

       *//* if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            //playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(rotation.x, rotation.y);

           *//* rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);          
            playerCameraPivot.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);          
            transform.rotation = Quaternion.Euler(0, rotation.y, 0);*//*
        }*//*
    }

}*/