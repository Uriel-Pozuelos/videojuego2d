using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUB : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI TextMeshProUGUI;

    [Header("Game Objects")]
    public GameObject[] gameObjects;
    public GameObject[] bullets;

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI.text = "Total coins: " + GameManager.Instance.TotalCoins.ToString();
    }

    public void UpdateCoins(int coins)
    {
        TextMeshProUGUI.text = "Total coins: " + coins.ToString();
    }

    public void AddBullet(int index)
    {
        if (index < 0 || index >= bullets.Length) return; // Asegura que el índice esté en un rango válido
        bullets[index].SetActive(true);
    }

    public void RestBullet(int index)
    {
        if (index < 0 || index >= bullets.Length) return; // Asegura que el índice esté en un rango válido
        bullets[index].SetActive(false);
    }

    public void RestLife(int index)
    {
        if (index < 0 || index >= gameObjects.Length) return; // Validación de rango para evitar errores
        gameObjects[index].SetActive(false);
    }

    public void AddLife(int index)
    {
        if (index < 0 || index >= gameObjects.Length) return; // Asegura que el índice esté en un rango válido
        gameObjects[index].SetActive(true);
    }
}
