using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;
    public Transform playerCamera; // Camera as child of player

    private float xRotation = 0f; // محدود کردن نگاه بالا/پایین

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // موس مخفی و قفل روی صفحه
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.y = 0f; // Y تغییر نکند
        move *= moveSpeed * Time.deltaTime;

        transform.position += move;
    }

    private bool Attack = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Woman")
        {
            Attack = true;
        }
    }
}