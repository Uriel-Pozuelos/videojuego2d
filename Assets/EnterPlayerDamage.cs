using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPlayerDamage : MonoBehaviour
{
    public float cooldownAtaque = 2f; // Tiempo entre ataques
    private bool puedeAtacar = true; // Controla si las púas pueden hacer daño

    void Start()
    {
        // Inicializa si es necesario
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Verificamos si el objeto que tocó las púas es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            // Si no puede atacar, salimos de la función
            if (!puedeAtacar) return;

            // Desactivamos el ataque para evitar daños múltiples en un corto tiempo
            puedeAtacar = false;

            // Reducimos la vida del jugador a través del GameManager
            GameManager.Instance.lostLife();

            // Si el jugador tiene una función para manejar el daño, la llamamos
            other.gameObject.GetComponent<PlayerMove>().hittingByEnemy();

            // Esperamos el tiempo de cooldown antes de permitir otro ataque
            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    void ReactivarAtaque()
    {
        // Reactiva la capacidad de hacer daño después del cooldown
        puedeAtacar = true;
    }
}
