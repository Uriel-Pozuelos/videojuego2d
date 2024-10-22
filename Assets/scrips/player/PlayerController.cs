using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables públicas
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 5f;
    public Transform groundCheck; // Un punto bajo el jugador para verificar el suelo
    public float checkRadius = 0.1f; // Radio para detectar el suelo
    public LayerMask groundLayer; // Asigna esto a la capa del suelo
    public float hitForce = 5f;
    private bool canMove = true;

    // Componentes
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Estados
    private bool isGrounded;
    private bool isRunning;
    private bool isJumping;

    // Variables privadas
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimation();
    }

    void HandleMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Correr al mantener Shift
        isRunning = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        // Cambiar la dirección del sprite
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }
    }

    void HandleJump()
    {
        // Verificar si el personaje está tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Saltar al presionar Espacio
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }

        if (isGrounded && rb.velocity.y <= 0)
        {
            isJumping = false;
        }
    }

    void UpdateAnimation()
    {

        if(!canMove)
        {
            return;
        }

        if (moveInput == 0 && !isJumping)
        {
            animator.SetInteger("anim-state", 0);
        }
        else if (moveInput != 0 && !isRunning && !isJumping)
        {
            animator.SetInteger("anim-state", 1);
        }
        else if (moveInput != 0 && isRunning && !isJumping)
        {
            animator.SetInteger("anim-state", 2);
        }
        else if (isJumping)
        {
            animator.SetInteger("anim-state", 3);
        }
    }

    // Dibuja el área de detección del suelo para depuración
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }



}
