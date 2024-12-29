using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemigo
{
    [SerializeField] private float danoAtaque;

    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            Debug.Log("Player detectado");
        }
        else if (elOtro.gameObject.CompareTag("PlayerHitBox"))
        {
            SistemaVidas sistemaVidasPlayer = elOtro.gameObject.GetComponent<SistemaVidas>();
            sistemaVidasPlayer.RecibirDano(danoAtaque);
        }
    }

    public override void Atacar()
    {
        anim.SetTrigger("atacar");
    }

    public override void Perseguir()
    {
        anim.SetBool("sorpresa", true);
    }

    public override void Morir(float tiempoDestruccion)
    {
        tiempoDestruccion = 0.5f;
        base.Morir(tiempoDestruccion);
        anim.SetTrigger("explosion");
    }
}
