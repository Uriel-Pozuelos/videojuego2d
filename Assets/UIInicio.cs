using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInicio : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Botones")]
    public GameObject botonJugar;
    public GameObject botonSalir;
    public GameObject botonInstrucciones;


    public void Jugar() {
        SceneManager.LoadScene("nivel_test");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Instrucciones()
    {
        SceneManager.LoadScene("Instrucciones");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
