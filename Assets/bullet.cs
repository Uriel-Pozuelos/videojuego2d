using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float velocity = 10.0f;
   


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right * velocity * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            // Reducimos la vida del jugador a través del GameManager
            GameManager.Instance.lostLife();

            // Si el jugador tiene una función para manejar el daño, la llamamos
            collision.gameObject.GetComponent<PlayerMove>().hittingByEnemy();

        }
        Destroy(gameObject);

    }
 }
