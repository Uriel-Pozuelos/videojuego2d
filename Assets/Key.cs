using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Colisi�n con " + other.gameObject.name); // Mensaje de aviso
        // Asumiendo que el jugador tiene un tag llamado "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Has recogido la llave."); // Mensaje de aviso
            GameManager.Instance.OnCoin(); // A�adir una moneda al contador
            GameManager.Instance.AllowNextLevel(); // Permitir avanzar al siguiente nivel
            Destroy(gameObject); // Destruir la llave despu�s de recogerla
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisi�n con " + collision.gameObject.name); // Mensaje de aviso
        // Asumiendo que el jugador tiene un tag llamado "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Has recogido la llave."); // Mensaje de aviso
            GameManager.Instance.OnCoin(); // A�adir una moneda al contador
            GameManager.Instance.AllowNextLevel(); // Permitir avanzar al siguiente nivel
            Destroy(gameObject); // Destruir la llave despu�s de recogerla
        }
    }
}
