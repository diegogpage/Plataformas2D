using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;

    protected float Vida { get => vida; set => vida = value; }

    protected abstract void Atacar(); //No está clara la forma de atacar (cambia para cada uno)
    //Como no todos atacan igual, la pongo abstracta y luego en cada script indico como lo hago

    protected virtual void Perseguir() //Hay una parte que es común a todos los enemigos
    {
        //1. Calcular durección al objetivo
        //2. Disponernos a movernos en esa direccion.
        //En este caso como el mago no gira lo unico comun seria orientarse hacia el objetivo y pasar a 
        //atacar cuand esté a cierta distancia (si la distancia cambia según el enemigo ponerlo en cada uno)
    }

    protected void TakeDamage(float damage)
    {

    }
}
