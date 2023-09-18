using UnityEngine;

public class MouseAim : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float walkingBobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;

    float xRotation = 0f;
    float timer = 0;

    // Store the initial local position of the camera
    Vector3 initialLocalPosition;

    public Transform playerBody;

    // Modify this to indicate if the player is moving
    bool isMoving = false;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Store the initial local position
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        HandleMouseAim();
        HandleBobbing();
    }

    void HandleMouseAim()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, transform.localRotation.z);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void HandleBobbing()
    {
        // Modify this to indicate if the player is moving
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            isMoving = true;
            timer += Time.deltaTime * walkingBobbingSpeed;
        }
        else
        {
            isMoving = false;
            timer = 0;
        }

        // Calculate the bobbing effect
        float bobbingOffset = isMoving ? Mathf.Sin(timer) * bobbingAmount : 0f;
        transform.localPosition = new Vector3(initialLocalPosition.x, initialLocalPosition.y + bobbingOffset, initialLocalPosition.z);
    }
}