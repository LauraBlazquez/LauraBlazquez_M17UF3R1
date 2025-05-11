using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public Transform thirdPersonPivot;
    public Transform firstPersonPivot;
    public Transform dancePivot;
    public Transform playerBody;
    public float cameraSpeed = 3f;
    public float mouseSensitivity = 0.3f;
    public float minY = -90f;
    public float maxY = 90f;
    public Player player;
    public float danceAnimationDuration = 3f;
    public List<GameObject> itemsToHide = new List<GameObject>();

    private PlayerControlls controls;
    private bool isAiming;
    private bool isDancing;
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
        controls.Gameplay.Dance.performed += ctx => Dance();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Camera.Enable();
    }

    private void OnDisable()
    {
        controls.Camera.Look.performed -= ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Camera.Look.canceled -= ctx => lookInput = Vector2.zero;
        controls.Gameplay.Aim.performed -= ctx => isAiming = true;
        controls.Gameplay.Aim.canceled -= ctx => isAiming = false;
        controls.Gameplay.Dance.performed -= ctx => Dance();
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
        if (!isDancing)
        {
            currentTarget = isAiming ? firstPersonPivot : thirdPersonPivot;
        }
        transform.position = Vector3.Lerp(transform.position, currentTarget.position, Time.deltaTime * cameraSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentTarget.rotation, Time.deltaTime * cameraSpeed);
    }

    private void Update()
    {
        if (!isDancing)
        {
            float mouseX = lookInput.x * mouseSensitivity;
            float mouseY = lookInput.y * mouseSensitivity;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minY, maxY);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void Dance()
    {
        if (!isDancing)
        {
            player.GetComponent<Animator>().SetTrigger("Dance");
            foreach (GameObject item in itemsToHide)
            {
                item.SetActive(false);
            }
            StartCoroutine(DanceRoutine());
        }
    }

    private IEnumerator DanceRoutine()
    {
        isDancing = true;
        currentTarget = dancePivot;
        yield return new WaitForSeconds(danceAnimationDuration);
        isDancing = false;
        foreach (GameObject item in itemsToHide)
        {
            item.SetActive(true);
        }
    }
}