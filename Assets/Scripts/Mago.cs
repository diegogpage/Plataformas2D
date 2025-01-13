using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Mago : Enemigo
{

    [SerializeField] private BolaFuego bolaFuegoPrefab;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private int danhoAtaque;
    [SerializeField] private Player target;
    private Animator anim;

    //Creo la pool para las bolas de fuego
    private ObjectPool<BolaFuego> poolBolas;


    private void Awake()
    {
        poolBolas = new ObjectPool<BolaFuego>(CrearBola, GetBola, ReleaseBola, DestroyBola);
    }

    private BolaFuego CrearBola()
    {
        BolaFuego copiaBola = Instantiate(bolaFuegoPrefab, puntoSpawn.position, transform.rotation);
        copiaBola.MyPoolBolas = poolBolas;
        return copiaBola;
    }

    private void GetBola(BolaFuego bola)
    {
        bola.transform.position = puntoSpawn.position;
        bola.transform.rotation = transform.rotation;
        bola.gameObject.SetActive(true);
    }

    private void ReleaseBola(BolaFuego bola)
    {
        bola.gameObject.SetActive(false);
    }

    private void DestroyBola(BolaFuego bola)
    {
        Destroy(bola.gameObject);
    }



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    //Lanzo desde animacion
    private void LanzarBola()
    {
        poolBolas.Get();
        //Instantiate(bolaFuegoPrefab, puntoSpawn.position, transform.rotation);
        //Las saco con la rotación del mago por si este mira a la izquierda o a la derecha
    }

    public override void Perseguir()
    {
        Debug.Log("Te veo llegar");
    }

    public override void Morir(float tiempoDestruccion)
    {
        tiempoDestruccion = 0.1f;
        base.Morir(tiempoDestruccion);
        Debug.Log("Dead");
    }

    public override void Atacar()
    {
        anim.SetTrigger("atacar"); //Lanzo desde aquí para que vaya enfocando al jugador
        Debug.Log("Lanzo");
    }

}
