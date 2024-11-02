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
    public int TotalCoins = 0; // Cambiado a público

    [Header("Game Mechanics")]
    public bool canNextLevel = false; // Indica si el jugador puede avanzar al siguiente nivel

    [Header("Audio Clips")]
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
            StartCoroutine(AssignHub()); // Iniciar corrutina para asignar el HUB
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
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
        StartCoroutine(AssignHub()); // Iniciar corrutina al cargar una escena
        ResetLevelSettings(); // Restablece las configuraciones del nivel
        if (hub != null)
        {
            hub.RestLife(Life); // Actualizar la UI de vida en el HUB
        }
    }

    private void Start()
    {
       
    }

    // Corrutina para esperar y asignar el HUB
    private IEnumerator AssignHub()
    {
        yield return new WaitForSeconds(0.1f); // Espera breve para dar tiempo a que el HUB se cargue
        hub = FindObjectOfType<HUB>();
        if (hub == null)
        {
            Debug.LogWarning("Hub no asignado en GameManager");
        }
    }

    // Método para restablecer las configuraciones específicas del nivel
    private void ResetLevelSettings()
    {
        Life = TotalLife; // Vida máxima para el nivel actual
        bullets = 5; // Restablecer balas al máximo
        TotalCoins = 0; // Reiniciar monedas para el nivel
        canNextLevel = false; // Reiniciar permiso de avance de nivel
        // Actualizar la UI del HUD
        if (hub != null)
        {
            hub.UpdateCoins(TotalCoins);
            hub.RestBullet(bullets);
            hub.RestLife(Life);
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

    public void AddCoins()
    {
        TotalCoins += 1;
        audioSource.PlayOneShot(onCoinSound);


        if(TotalCoins % 3 == 0 && bullets < 5)
        {
            addBullet();
        }


        if (hub != null)
        {
            hub.UpdateCoins(TotalCoins);
        }
        Debug.Log("Total coins: " + TotalCoins);
    }

    public void lostLife()
    {
        if (Life > 0)
        {
            Life -= 1;
            if (hub != null)
            {
                hub.RestLife(Life);
            }
            PlayOnDeadSound();

            if (Life <= 0)
            {
                Debug.Log("Game Over");
                audioSource.PlayOneShot(onLoseSound);
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void lessBullet()
    {
        if (bullets > 0)
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
            SceneManager.LoadScene("GameOver");
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
                hub.AddLife(Life - 1);
            }
        }
    }

    public void OnWinLevel(string nextScene = null)
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
        canNextLevel = true;
    }
}
