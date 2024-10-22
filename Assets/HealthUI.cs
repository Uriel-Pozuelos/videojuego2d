using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab; // Prefab del corazón (Imagen)
    public Transform heartContainer; // Contenedor donde estarán los corazones
    public int maxHealth = 5; // Máxima cantidad de corazones (vida máxima)
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Establecemos la vida actual como el máximo al inicio
        UpdateHearts();
    }

    // Método para actualizar la UI de corazones
    public void UpdateHearts()
    {

        Debug.Log("Vida actual: " + currentHealth);
        // Limpiar corazones antiguos
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }

        // Añadir los corazones necesarios
        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(heartPrefab, heartContainer); // Instanciar el prefab del corazón
        }
    }

    // Método para reducir la vida del jugador
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHearts(); // Actualizar la UI cuando se toma daño
    }

    // Método para curar al jugador
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHearts(); // Actualizar la UI cuando se cura
    }
}
