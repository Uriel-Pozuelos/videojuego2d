using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtenemos el punto de contacto
            ContactPoint2D contact = collision.contacts[0];

            // Si el punto de contacto está por encima del enemigo
            if (contact.point.y > transform.position.y)
            {
                // Destruir al enemigo
                Destroy(gameObject);

                // Si quieres agregar alguna acción adicional, como un rebote para el jugador
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    // Aplicar un impulso hacia arriba
                    playerRb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
                }
            }
        }
    }
}
