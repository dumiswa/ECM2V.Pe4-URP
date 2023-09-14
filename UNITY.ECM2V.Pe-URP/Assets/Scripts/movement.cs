using UnityEngine;

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

        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            //playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(rotation.x, rotation.y);
        }
    }

}