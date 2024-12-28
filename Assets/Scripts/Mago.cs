using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : Enemigo
{

    [SerializeField] private GameObject bolaFuegoPrefab;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaque;
    [SerializeField] private float danhoAtaque;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RutinaAtaque());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RutinaAtaque()
    {
        while (true) //Bucle infinito
        {
            anim.SetTrigger("atacar"); //Pongo la animación
            yield return new WaitForSeconds(tiempoAtaque);
        }
    }

    private void LanzarBola()
    {
        //Desde un evento en la animación instancio las bolas
        Instantiate(bolaFuegoPrefab, puntoSpawn.position, transform.rotation);
        //Las saco con la rotación del mago por si este mira a la izquierda o a la derecha
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
            sistemaVidasPlayer.RecibirDano(danhoAtaque);
        }
    }

    protected override void Atacar()
    {
        Debug.Log("Mago ataca");
    }

    protected override void Perseguir()
    {
        base.Perseguir();
        //Si llegas a X distancia ataca
    }
}
