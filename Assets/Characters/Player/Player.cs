using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float moveSpeed = 3.5f;
    public float runSpeed = 14f;
    public float jumpForce = 5f;
    private PlayerControlls controls;
    private Vector2 movementInput;
    private Rigidbody rb;
    public Animator animator;
    private bool isRunning;
    public bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControlls();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Walk.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Walk.canceled += ctx => movementInput = Vector2.zero;
        controls.Gameplay.Run.performed += ctx => isRunning = true;
        controls.Gameplay.Run.canceled += ctx => isRunning = false;
        controls.Gameplay.Shoot.performed += ctx => animator.SetTrigger("Shoot");
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Crouch.performed += ctx => animator.SetTrigger("Crouch");
        controls.Gameplay.Crouch.canceled += ctx => animator.SetTrigger("Stand");
    }

    private void OnDisable()
    {
        controls.Gameplay.Walk.performed -= ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Walk.canceled -= ctx => movementInput = Vector2.zero;
        controls.Gameplay.Run.performed -= ctx => isRunning = true;
        controls.Gameplay.Run.canceled -= ctx => isRunning = false;
        controls.Gameplay.Shoot.performed -= ctx => animator.SetTrigger("Shoot");
        controls.Gameplay.Jump.performed -= ctx => Jump();
        controls.Gameplay.Crouch.performed -= ctx => animator.SetTrigger("Crouch");
        controls.Gameplay.Crouch.canceled -= ctx => animator.SetTrigger("Stand");
        controls.Gameplay.Disable();
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        float currentSpeed = isRunning ? runSpeed : moveSpeed;
        rb.velocity = new Vector3(move.x * currentSpeed, rb.velocity.y, move.z * currentSpeed);

        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 1.1f);

        if (movementInput != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", isRunning);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            isGrounded = false;
        }
    }

    //public void TakeDamage(float damage)
    //{
    //    currentHealth -= damage;
    //    //Debug.Log($"Player took {damage} damage. Health left: {currentHealth}");

    //    if (currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}

    //private void Die()
    //{
    //    //Debug.Log("Player died!");
    //    Destroy(gameObject);
    //}
}