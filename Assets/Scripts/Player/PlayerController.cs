using System.Security.Cryptography;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    public float playerHp = 100;
    [Header("Movement Options")]
    public float jumpPower = 50f;
    public float speed = 5f;
    [SerializeField] private float distance = 20f;
    Rigidbody2D rb;
    public CapsuleCollider2D normalcollider;
    [Header("EnemyCollider")]
    [SerializeField] BoxCollider2D col;
    [SerializeField] float smallsize = 0.5f;
    bool isGrounded = true;
    bool isRolling = false;
    Vector2 colliderSize;
    Vector2 colliderCenter;
    private PlayerControls controls;
    [SerializeField] GameObject inventory;
    private Vector2 moveInput;
    [SerializeField] private LayerMask ground;
    float movement;
    [HideInInspector] public bool collecting;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        normalcollider = GetComponent<CapsuleCollider2D>();
        colliderSize = normalcollider.size;
        colliderCenter = normalcollider.offset;
        controls = new PlayerControls();
        
    }

    private void Start()
    {
        inventory.SetActive(false);
    }
    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Roll.performed += ctx => StartCoroutine(PerformRoll());
        controls.Player.Collect.performed += Collect;
        controls.Player.Inventory.performed += ToggleInventory;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Jump.performed -= ctx => Jump();
        controls.Player.Roll.performed -= ctx => StartCoroutine(PerformRoll());
    }
    private void Update()
    {

        moveInput = controls.Player.Movement.ReadValue<Vector2>();
        HandleRotation();
    }

    private void FixedUpdate()
    {

        if (isRolling)
            return;
        GroundChecker();
        
        if (isGrounded)
        {
            Move();
        }
    }

    void Move()
    {
        movement = moveInput.x;
        rb.linearVelocity = new Vector2 (movement * speed ,rb.linearVelocity.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
       
    }
     public void Collect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            collecting = true;
        }
        else if (context.canceled)
        {
            collecting = false;
        }
    }
    System.Collections.IEnumerator PerformRoll()
    {
        isRolling = true;
        Debug.Log("Roll Baþladý");
        col.enabled = false;
        Vector2 newsize = normalcollider.size;
        newsize.y -= smallsize;
        normalcollider.size = newsize;
        //merkez deðiþtirme
        Vector2 newCenter = normalcollider.offset;
        newCenter.y -= (smallsize / 2);
        normalcollider.offset = newCenter; 
        
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        float facingDirection = Mathf.Sign(transform.localScale.x);
        rb.linearVelocity = new Vector2(facingDirection * distance, rb.linearVelocity.y);
        yield return new WaitForSeconds(0.6f);
        normalcollider.size = colliderSize;
        normalcollider.offset = colliderCenter;
        col.enabled = true;
        isRolling = false;
        Debug.Log("Roll Bitti");
    }

    void HandleRotation()
    {
        Vector3 currentScale = transform.localScale;

        if (movement < 0)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
        }
        else if (movement > 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        transform.localScale = currentScale;
    }

    void GroundChecker()
    {
        RaycastHit2D hittt = Physics2D.BoxCast(col.bounds.center,col.bounds.size,0f,Vector2.down,0.1f,ground);
        isGrounded = hittt.collider != null;
    }

    public void ToggleInventory(InputAction.CallbackContext contextt)
    {
        if (contextt.performed)
        {
            bool isOpen = inventory.activeSelf;
            inventory.SetActive(!isOpen);
            if (inventory.activeSelf)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f; 
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}