using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;

    protected float Vida { get => vida; set => vida = value; }

    public abstract void Atacar(); //No est� clara la forma de atacar (cambia para cada uno)
    //Como no todos atacan igual, la pongo abstracta y luego en cada script indico como lo hago
    //Si tuviesen parte en comun todos ser�a virtual

    public abstract void Perseguir();

    public virtual void Morir(float tiempoDestruccion)
    {
        Debug.Log("Muerto");
        Invoke("Destruir", tiempoDestruccion);
    }

    protected void Destruir()
    {
        Destroy(this.gameObject);
    }
    
}
