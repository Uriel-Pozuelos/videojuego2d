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

    void Start()
    {
        // No necesitas inicializar jugadorDetectado en Start, así que lo puedes eliminar.
    }

    void Update()
    {
        // Disparar automáticamente en intervalos
        if (Time.time > lastBullet + delayBullet)
        {
            Disparar(); // Dispara
            lastBullet = Time.time; // Actualiza el tiempo del último disparo
        }

        // Si deseas mostrar la detección del jugador, puedes dejarlo aquí.
        // jugadorDetectado = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaDisparo, capaJugador);
    }

    public void Disparar()
    {
        Instantiate(bullet, controladorDisparo.position, controladorDisparo.rotation);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaDisparo);
    }
}
