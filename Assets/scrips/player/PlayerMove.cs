using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movimiento")]
    private float Horizontalmove = 0f;
    [SerializeField] private float runSpeed;
    [Range(0, 0.3f)][SerializeField] private float smoothVelocity;
    public float hitForce = 5f;

    private Vector3 velocity = Vector3.zero;

    public bool canMove = true;
    private bool facingRight = true;

    [Header("Salto")]
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 groundCheckSize;
    [SerializeField] private bool isGrounded;
    [SerializeField] private AudioClip onJumpSound; // Clip de audio para el salto
    private AudioSource audioSource; // Componente AudioSource para reproducir el sonido de salto

    private bool isJumping = false;

    [Header("Animaciones")]
    private Animator animator;

    [Header("bullet")]
    public GameObject bullet;
    public float delay = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>(); // Añade el componente AudioSource al jugador
    }

    private void Update()
    {
        Horizontalmove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Horizontal", Mathf.Abs(Horizontalmove));

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
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
            PlayJumpSound(); // Reproduce el sonido de salto al saltar
        }
    }

    private void PlayJumpSound()
    {
        if (onJumpSound != null)
        {
            audioSource.PlayOneShot(onJumpSound); // Reproduce el sonido del salto una sola vez
        }
    }


    private void Shoot()
    {
        if (GameManager.Instance.bullets > 0)
        {
            GameManager.Instance.OnShoot();

            // Determina la dirección de la bala basado en la orientación del jugador
            Vector2 bulletDirection = facingRight ? Vector2.right : Vector2.left;

            // Instancia la bala
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

            // Establece la dirección de la bala usando el script `BulletPlayer`
            BulletPlayer bulletScript = newBullet.GetComponent<BulletPlayer>();
            bulletScript.SetDirection(bulletDirection); // Envía la dirección al script de la bala

            GameManager.Instance.lessBullet();
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

    public void hittingByEnemy()
    {
        Vector2 hitDireccion;
        if (rb.velocity.x > 0)
        {
            hitDireccion = new Vector2(-1f, 1f);
        }
        else
        {
            hitDireccion = new Vector2(1f, 1f);
        }

        rb.AddForce(hitDireccion * hitForce, ForceMode2D.Impulse);
        StartCoroutine(WaitAndActiveMovement());
    }

    IEnumerator WaitAndActiveMovement()
    {
        yield return new WaitForSeconds(0.1f);
        while (!isGrounded)
        {
            yield return null;
        }
        canMove = true;
    }
}
