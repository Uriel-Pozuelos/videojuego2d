using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float velocity = 10.0f;
    private Vector2 direction;

    // Método para configurar la dirección de la bala
    public void SetDirection(Vector2 shootDirection)
    {
        direction = shootDirection.normalized; // Normaliza la dirección para mantener velocidad constante
    }

    void Update()
    {
        // Mueve la bala en la dirección establecida al disparar
        transform.Translate(direction * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);
        if (collision.gameObject.name == "DINO" || collision.gameObject.name == "Bat")
        {
            GameManager.Instance.OnkillEnemy(); // Aumenta el contador de enemigos eliminados
            Destroy(collision.gameObject); // Destruye el objeto enemigo
            Destroy(gameObject); // Destruye la bala
        }
    }
}
