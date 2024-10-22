using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("Moviviento")]
    private float Horizontalmove = 0f;

    [SerializeField] private float runSpeed;
    [Range(0,0.3f)][SerializeField] private float smoothVelocity;

    private Vector3 velocity = Vector3.zero;


    private bool facingRight = true;

    [Header("Salto")]
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 groundCheckSize;
    [SerializeField] private bool isGrounded;

    bool isJumping = false;


    [Header("Animaciones")]
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Horizontalmove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Horizontal", Mathf.Abs(Horizontalmove));

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        animator.SetBool("canJump", !isGrounded);

        MoveCharacter(Horizontalmove * Time.fixedDeltaTime, isJumping);

        isJumping = false;
    }

    private void MoveCharacter(float move, bool isJumping)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothVelocity);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        if (isJumping && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
