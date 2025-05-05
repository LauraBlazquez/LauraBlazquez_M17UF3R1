using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform thirdPersonPivot;
    public Transform firstPersonPivot;
    public Transform playerBody;
    public float cameraSpeed = 3f;
    public float mouseSensitivity = 0.3f;
    public float minY = -90f;
    public float maxY = 90f;

    private PlayerControlls controls;
    private bool isAiming;
    private Vector2 lookInput;
    private float xRotation = 0f;

    private Transform currentTarget;

    private void Awake()
    {
        controls = new PlayerControlls();
        controls.Camera.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Camera.Look.canceled += ctx => lookInput = Vector2.zero;
        controls.Gameplay.Aim.performed += ctx => isAiming = true;
        controls.Gameplay.Aim.canceled += ctx => isAiming = false;
    }


    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Camera.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        currentTarget = thirdPersonPivot;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        currentTarget = isAiming ? firstPersonPivot : thirdPersonPivot;
        transform.position = Vector3.Lerp(transform.position, currentTarget.position, Time.deltaTime * cameraSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentTarget.rotation, Time.deltaTime * cameraSpeed);
    }

    private void Update()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minY, maxY);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
