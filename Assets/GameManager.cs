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

    [Header("Audio Clips")]
    public AudioClip sceneSound;      // Sonido para la escena
    public AudioClip onDeadSound;     // Sonido al perder una vida
    public AudioClip onCoinSound;     // Sonido al recoger una moneda
    private AudioSource audioSource;   // Componente de AudioSource para reproducir sonidos

    // Asegura que la instancia no se destruya entre escenas
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>(); // Añadir AudioSource al GameManager
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlaySceneSound(); // Reproducir el sonido de la escena al iniciar
    }

    public void AddCoins(int coins)
    {
        totalCoins += coins;
        audioSource.PlayOneShot(onCoinSound); // Reproducir el sonido al recoger monedas
        hub.UpdateCoins(totalCoins);
        Debug.Log("Total coins: " + totalCoins);
    }

    public void lostLife()
    {
        Life -= 1;
        hub.RestLife(Life);
        PlayOnDeadSound(); // Reproducir el sonido al perder vida
        if (Life <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(0);
        }
    }

    public void killPlayer()
    {
        Life = 0;
        hub.RestLife(Life);
        PlayOnDeadSound(); // Reproducir el sonido al perder vida
        if (Life <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(0);
        }
    }

    public void AddLife()
    {
        if (Life == TotalLife)
        {
            return;
        }

        if (Life < TotalLife)
        {
            hub.AddLife(Life);
            Life += 1;
        }
    }

    private void PlaySceneSound()
    {
        if (sceneSound != null)
        {
            audioSource.clip = sceneSound;
            audioSource.Play();
        }
    }

    private void PlayOnDeadSound()
    {
        if (onDeadSound != null)
        {
            audioSource.PlayOneShot(onDeadSound);
        }
    }
}
