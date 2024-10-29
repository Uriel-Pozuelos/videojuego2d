using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float cooldownAtaque;
    public bool movimientoInfinito = true; // Activa o desactiva el movimiento aleatorio en X
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private float velocidadMovimiento = 2f;
    private float direccionMovimiento; // Variable para almacenar la dirección de movimiento

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb == null)
        {
            Debug.LogError("No se encontró Rigidbody2D en el enemigo.");
        }

        // Inicia la corrutina de movimiento infinito
        StartCoroutine(MovimientoInfinito());
    }

    void Update()
    {
       
            // Establece la velocidad en el eje X en función de la dirección
            rb.velocity = new Vector2(direccionMovimiento * velocidadMovimiento, rb.velocity.y);
        
    }

    private IEnumerator MovimientoInfinito()
    {
        while (movimientoInfinito)
        {
            animator.SetInteger("anim-state", 1); // Cambia a animación de "run"

            // Asigna una dirección aleatoria
            direccionMovimiento = Random.Range(0, 2) == 0 ? -1f : 1f;

            // Cambia la dirección del sprite según el valor de dirección
            transform.localScale = new Vector3(direccionMovimiento, 1f, 1f);

            // Cambia la dirección cada cierto tiempo aleatorio para hacer el movimiento más dinámico
            float tiempoCambioDireccion = Random.Range(5f, 8f);
            yield return new WaitForSeconds(tiempoCambioDireccion);
        }

        // Si se desactiva movimientoInfinito, el enemigo se detiene en estado "idle"
        rb.velocity = Vector2.zero;
        animator.SetInteger("anim-state", 0); // Estado "idle"
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!puedeAtacar) return;

            puedeAtacar = false;

            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            GameManager.Instance.lostLife();

            other.gameObject.GetComponent<PlayerMove>().hittingByEnemy();

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    void ReactivarAtaque()
    {
        puedeAtacar = true;
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }
}
