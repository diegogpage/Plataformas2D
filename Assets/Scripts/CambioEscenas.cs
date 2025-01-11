using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JuegoFacil()
    {
        SceneManager.LoadScene("JuegoFacil");
    }

    public void JuegoDificil()
    {
        SceneManager.LoadScene("JuegoDificil");
    }

    public void Dificultad()
    {
        SceneManager.LoadScene("Dificultad");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Controles()
    {
        SceneManager.LoadScene("Controles");
    }

    public void Info()
    {
        SceneManager.LoadScene("Info");
    }
}
