using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Asumiendo que el jugador tiene un tag llamado "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Comprobar si el jugador puede avanzar al siguiente nivel
            if (GameManager.Instance.canNextLevel)
            {
                GameManager.Instance.OnWinLevel(); // Intentar avanzar al siguiente nivel
            }
            else
            {
                Debug.Log("Necesitas la llave para avanzar al siguiente nivel."); // Mensaje de aviso
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Asumiendo que el jugador tiene un tag llamado "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Comprobar si el jugador puede avanzar al siguiente nivel
            if (GameManager.Instance.canNextLevel)
            {
                GameManager.Instance.OnWinLevel(); // Intentar avanzar al siguiente nivel
            }
            else
            {
                Debug.Log("Necesitas la llave para avanzar al siguiente nivel."); // Mensaje de aviso
            }
        }
    }
}
