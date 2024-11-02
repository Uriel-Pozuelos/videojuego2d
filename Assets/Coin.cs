using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desactivar el collider para evitar múltiples colisiones
            GetComponent<Collider2D>().enabled = false;

            // Desactivar la moneda visualmente
            gameObject.SetActive(false);

            // Agregar moneda en el GameManager
            GameManager.Instance.AddCoins();

            // Destruir el objeto después de un pequeño retraso
            Destroy(gameObject, 0.1f);
        }
    }


}
