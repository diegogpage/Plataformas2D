using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private ParticleSystem particulas;
    private bool cambioEscena;
    private float timer;
    private float timerCambio;

    // Start is called before the first frame update
    void Start()
    {
        cambioEscena = false;
        timer = 0;
        timerCambio = 2;
        particulas = GetComponentInChildren<ParticleSystem>();

        if (particulas != null)
        {
            particulas.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CambioEscena();
    }

    private void CambioEscena()
    {
        if (cambioEscena)
        {
            Debug.Log("Cambio");
            timer += Time.deltaTime;

            if(timer >= timerCambio)
            {
                SceneManager.LoadScene("PantallaFinal");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
            if(particulas != null)
            {
                particulas.Play();
                cambioEscena = true;
            }
        }
    }
}
