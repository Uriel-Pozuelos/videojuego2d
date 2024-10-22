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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     TextMeshProUGUI.   text = "Total coins: " + GameManager.Instance.TotalCoins.ToString();
    }

    public void UpdateCoins(int coins)
    {
        TextMeshProUGUI.text = "Total coins: " + coins.ToString();
    }
  
    public void RestLife(int index)
    {
        gameObjects[index].SetActive(false);
    }

    public void AddLife(int index)
    {
        if (index < 0 || index >= gameObjects.Length) return; // Asegura que el índice esté en un rango válido
        gameObjects[index].SetActive(true);
    }
}

