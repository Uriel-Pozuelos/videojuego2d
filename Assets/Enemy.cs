using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float cooldownAtaque;
    public float jumpForce = 10f; // Aumenta la fuerza de salto
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Verificamos si el Rigidbody2D está configurado correctamente
        if (rb == null)
        {
            Debug.LogError("No se encontró Rigidbody2D en el enemigo.");
        }
        else
        {
            // Inicia el ciclo de saltos cada 2 segundos
            InvokeRepeating("Jump", 2f, 2f);
        }
    }

    private void Jump()
    {
        if (rb != null)
        {
            // Aplica una fuerza hacia arriba para que el enemigo salte
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("El enemigo ha saltado."); // Log para verificar si el salto ocurre
        }
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
