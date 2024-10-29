using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullajeOrden : MonoBehaviour
{

    [SerializeField] private Transform[] puntos;
    [SerializeField] private float velocidad;
    [SerializeField] private float distanciaMinima;
    private int indice = 0;

    private SpriteRenderer spriteRenderer;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve el objeto hacia el punto objetivo
        transform.position = Vector2.MoveTowards(transform.position, puntos[indice].position, velocidad * Time.deltaTime);

        // Comprueba si ha llegado lo suficientemente cerca del punto
        if (Vector2.Distance(transform.position, puntos[indice].position) < distanciaMinima)
        {
            // Cambia el índice a la siguiente posición
            indice++;
            // Si ha alcanzado el final del array, vuelve al inicio
            if (indice >= puntos.Length)
            {
                indice = 0;
            }
            // Gira el sprite para que mire en la dirección del siguiente punto
            Girar();
        }
    }


    private void Girar()
    {
        // Comprueba si el sprite debe girar a la izquierda o a la derecha
        if (transform.position.x < puntos[indice].position.x)
        {
            spriteRenderer.flipX = true; // Mira a la derecha
        }
        else if (transform.position.x > puntos[indice].position.x)
        {
            spriteRenderer.flipX = false; // Mira a la izquierda
        }
    }

}
