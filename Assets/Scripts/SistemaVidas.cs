using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;
    
    public void RecibirDano(float danoRecibido)
    {
        vidas -= danoRecibido;
        if(vidas <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
