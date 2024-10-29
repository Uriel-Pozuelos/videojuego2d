using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform floorController;
    [SerializeField] private float distance;
    [SerializeField] private bool isMovingRight;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D floorInfo = Physics2D.Raycast(floorController.position, Vector2.down, distance);
        rb.velocity = new Vector2(isMovingRight ? speed : -speed, rb.velocity.y); // Velocidad según dirección

        if (floorInfo.collider == null) // Si no hay colisión, gira
        {
            Turn();
        }
    }

    private void Turn()
    {
        isMovingRight = !isMovingRight;
        transform.eulerAngles = new Vector3(0, isMovingRight ? 0 : 180, 0); // Gira la plataforma en dirección correcta
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(floorController.position, new Vector2(floorController.position.x, floorController.position.y - distance));
    }
}
