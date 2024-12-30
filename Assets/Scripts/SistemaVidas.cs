using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;
    [SerializeField] private Enemigo enemigo;
    private float tiempoDestruccion;


    public void RecibirDano(float danhoRecibido)
    {
        vidas -= danhoRecibido;
        if (vidas <= 0)
        {
            Debug.Log("Morido");
            enemigo.Morir(tiempoDestruccion);
        }

    }
}
