using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    public int TotalLife = 3; // Máxima vida
    private int Life; // Vida actual
    public int bullets = 5;

    [Header("Game Stats")]
    public HUB hub;
    public string startScene;
    public string nextScene;

    public int TotalCoins = 0; // Cambiado a público

    [Header("Game Mechanics")]
    public bool canNextLevel = false; // Indica si el jugador puede avanzar al siguiente nivel

    [Header("Audio Clips")]
    public AudioClip sceneSound;
    public AudioClip onDeadSound;
    public AudioClip onCoinSound;
    public AudioClip onWinSound;
    public AudioClip onLoseSound;
    public AudioClip onJumpSound;
    public AudioClip onHealthSound;
    public AudioSource audioSource;
    public AudioClip onPressButton;
    public AudioClip onKillEnemy;
    public AudioClip onShoot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            canNextLevel = false;
            AssignHub();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void OnkillEnemy()
    {
        audioSource.PlayOneShot(onKillEnemy);
    }

    public void OnPressButton()
    {
        audioSource.PlayOneShot(onPressButton);
    }

    public void OnShoot()
    {
        audioSource.PlayOneShot(onShoot);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignHub();
        // Reiniciar la vida del jugador al cargar la escena
        Life = TotalLife;
        if (hub != null)
        {
            hub.RestLife(Life); // Actualizar la UI de vida en el HUB
        }
    }

    private void Start()
    {
        PlaySceneSound();
    }

    private void AssignHub()
    {
        if (hub == null)
        {
            hub = FindObjectOfType<HUB>();
            if (hub == null)
            {
                Debug.LogWarning("Hub no asignado en GameManager");
            }
        }
    }

    public void AddCoins(int coins)
    {
        TotalCoins += coins; // Actualizado para usar la variable pública
        audioSource.PlayOneShot(onCoinSound);
        //cada dos monedas se añande una bala si no se tiene el maximo
        if (TotalCoins % 2 == 0 && bullets < 5)
        {
            addBullet();
        }


        if (hub != null)
        {
            hub.UpdateCoins(TotalCoins); // Usar TotalCoins aquí
        }
        Debug.Log("Total coins: " + TotalCoins);
    }

    public void lostLife()
    {
        if (Life > 0)
        {
            Life -= 1; // Reducir vida
            if (hub != null)
            {
                hub.RestLife(Life); // Actualizar la UI de vida en el HUB
            }
            PlayOnDeadSound();

            if (Life <= 0)
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene(startScene); // Recargar escena de inicio
            }
        }
    }



     public void lessBullet()
    {
        if(bullets > 0)
        {
            bullets -= 1;
            if (hub != null)
            {
                hub.RestBullet(bullets);
            }
        }
    }

    public void addBullet()
    {
        if (bullets < 5)
        {
            bullets += 1;
            if (hub != null)
            {
                hub.AddBullet(bullets - 1);
            }
        }
    }


    public void killPlayer()
    {
        Life = 0;
        if (hub != null)
        {
            hub.RestLife(Life);
        }
        PlayOnDeadSound();
        if (Life <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(startScene);
        }
    }

    public void AddLife()
    {
        if (Life < TotalLife)
        {
            Life += 1;
            audioSource.PlayOneShot(onHealthSound);
            if (hub != null)
            {
                hub.AddLife(Life - 1); // Asegurarse de que el índice sea el correcto
            }
        }
    }

    public void OnWinLevel()
    {
        if (canNextLevel && !string.IsNullOrEmpty(nextScene))
        {
            Debug.Log("Level Won! Loading next scene: " + nextScene);
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("No se puede avanzar al siguiente nivel. Se requiere una llave.");
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

    // Método para permitir avanzar al siguiente nivel
    public void AllowNextLevel()
    {
        canNextLevel = true; // Cambia el valor a verdadero
    }
}
