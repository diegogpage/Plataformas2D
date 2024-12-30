using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemigo
{
    [SerializeField] private float danhoAtaque;
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private LayerMask queEsDanhable;
    [SerializeField] private Player player;

    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D elOtro)
    //{
    //    if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
    //    {
    //        Debug.Log("Player detectado");
    //    }
    //    else if (elOtro.gameObject.CompareTag("PlayerHitBox"))
    //    {
    //        SistemaVidas sistemaVidasPlayer = elOtro.gameObject.GetComponent<SistemaVidas>();
    //        sistemaVidasPlayer.RecibirDano(danoAtaque);
    //        Atacar();
    //    }
    //}

    public override void Atacar()
    {
        anim.SetTrigger("atacar");
        //SistemaVidas sistemaVidasPlayer = elOtro.gameObject.GetComponent<SistemaVidas>();
        //sistemaVidasPlayer.RecibirDano(danoAtaque);
    }

    //Se lanza desde evento de animación
    private void Ataque()
    {
        //Lanzar trigger instantáneo (para que no esté siempre presente)
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);

        foreach (Collider2D item in collidersTocados)
        {
            //SistemaVidas sistemaVidasEnemigos = item.gameObject.GetComponent<SistemaVidas>();
            //sistemaVidasEnemigos.RecibirDano(danhoAtaque);
            player.quitarVidaPlayer(danhoAtaque);
        }

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
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }
}
