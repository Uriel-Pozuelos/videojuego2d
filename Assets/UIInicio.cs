using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInicio : MonoBehaviour
{
    public string restart;


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

    public void VolverMenu()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void Reset()
    {
        SceneManager.LoadScene(restart);
    }
    public void VolverInicio()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void GamerOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}
