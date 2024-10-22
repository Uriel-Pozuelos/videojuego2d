using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    [Header("Player Stats")]
    public int TotalLife = 3;
    private int Life = 3;


    [Header("Game Stats")]
    public HUB hub;

    public int TotalCoins
    {
        get { return totalCoins; }
    }

    private int totalCoins = 0;

    // Asegura que la instancia no se destruya entre escenas
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int coins)
    {
        totalCoins += coins;
        hub.UpdateCoins(totalCoins);
        Debug.Log("Total coins: " + totalCoins);
    }

    public void lostLife()
    {
        Life -= 1;
        hub.RestLife(Life);
        if (Life <= 0)
        {
           Debug.Log("Game Over");
            SceneManager.LoadScene(0);
        }
    }

    public void AddLife()
    {
        if(Life == TotalLife)
        {
            return;
        }

        if (Life < TotalLife)
        {
            hub.AddLife(Life);
            Life += 1;
        }
    }
}
