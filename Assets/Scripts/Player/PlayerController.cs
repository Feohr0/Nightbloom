using System.Security.Cryptography;
using Unity.Android.Gradle.Manifest;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    public float playerHp = 100;
    [Header("Movement Options")]
    public float jumpPower = 50f;
    public float speed = 5f;
    [SerializeField] private float distance = 20f;
    float movement;
    Rigidbody2D rb;
    [Header("EnemyCollider")]
    [SerializeField] CapsuleCollider2D col;
    bool isGrounded = true;
    bool isRolling = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (isRolling)
            return;
        CharacterMovement();
        HandleRotation();
    }

    void CharacterMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocityY = jumpPower;
        }

        movement = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(movement * speed, rb.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(PerformRoll());
        }
    }
    System.Collections.IEnumerator PerformRoll()
    {
        isRolling = true;
        Debug.Log("Roll Baþladý");
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        float facingDirection = Mathf.Sign(transform.localScale.x);
        col.size = new Vector2(1, 0.5f);
        rb.AddForce(new Vector2(facingDirection * distance * 20 , 0), ForceMode2D.Force);
        yield return new WaitForSeconds(0.6f);
        isRolling = false;
        Debug.Log("Roll Bitti");
    }

    void HandleRotation()
    {
        if (movement < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (movement > 0)
        {
            transform.localScale = new Vector2(1,1);
        }
    }

}