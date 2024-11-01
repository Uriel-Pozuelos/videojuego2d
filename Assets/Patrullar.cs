using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private Transform[] puntos;
    [SerializeField] private float velocidad;
    [SerializeField] private float distanciaMinima;
    private int indice;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        indice = Random.Range(0, puntos.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntos[indice].position, velocidad * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntos[indice].position) < distanciaMinima)
        {
            if (indice >= puntos.Length - 1)
            {
                indice = Random.Range(0, puntos.Length);
                Girar();
            }
            else
            {
                indice++;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }

    private void Girar()
    {
        if (transform.position.x < puntos[indice].position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cambiar de dirección al chocar con algo
        indice = (indice + 1) % puntos.Length; // Cambia al siguiente punto
        Girar(); // Actualiza la dirección
    }
}
