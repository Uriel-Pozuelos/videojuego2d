using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparoEnemigo : MonoBehaviour
{
    public Transform controladorDisparo; // Punto desde donde se dispararán las balas
    public float distanciaDisparo; // Distancia a la que se puede detectar al jugador
    public LayerMask capaJugador; // Capa en la que está el jugador
    public GameObject bullet; // Prefab de la bala
    public float delayBullet = 1f; // Tiempo de espera entre disparos

    private float lastBullet; // Tiempo del último disparo

    void Update()
    {
        // Disparar automáticamente en intervalos
        if (Time.time > lastBullet + delayBullet)
        {
            Disparar(); // Dispara
            lastBullet = Time.time; // Actualiza el tiempo del último disparo
        }
    }

    public void Disparar()
    {
        GameObject newBullet = Instantiate(bullet, controladorDisparo.position, controladorDisparo.rotation);
        Destroy(newBullet, 2f); // Destruir la bala después de 2 segundos
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaDisparo);
    }
}
