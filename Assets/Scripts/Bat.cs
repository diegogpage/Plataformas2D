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


    public override void Atacar()
    {
        anim.SetTrigger("atacar");
    }

    //Se lanza desde evento de animación
    private void Ataque()
    {
        //Lanzar trigger instantáneo (para que no esté siempre presente)
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);

        foreach (Collider2D item in collidersTocados)
        {
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    //}
}
