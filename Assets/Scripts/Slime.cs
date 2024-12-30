using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemigo
{

    [SerializeField] private float danhoAtaque;
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private LayerMask queEsDanhable;
    [SerializeField] private Player player;

    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void Atacar()
    {
        Debug.Log("Ataco");
        anim.SetBool("atacando", true);

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

        Debug.Log("Doy");

    }

    public override void Perseguir()
    {
        Debug.Log("Persigo");
        anim.SetBool("atacando", false);
    }

    public override void Morir(float tiempoDestruccion)
    {
        tiempoDestruccion = 0.1f;
        base.Morir(tiempoDestruccion);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }
}
