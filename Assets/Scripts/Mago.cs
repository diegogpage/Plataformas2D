using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : Enemigo
{

    [SerializeField] private GameObject bolaFuegoPrefab;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaque;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private Player target;
    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //StartCoroutine(RutinaAtaque());
    }

    // Update is called once per frame
    void Update()
    {
        EnfocarDestino();    
    }

    //Lanzo desde animacion
    private void LanzarBola()
    {
        Instantiate(bolaFuegoPrefab, puntoSpawn.position, transform.rotation);
        //Las saco con la rotación del mago por si este mira a la izquierda o a la derecha
    }

    public override void Perseguir()
    {
        Debug.Log("Te veo llegar");
    }

    public override void Morir(float tiempoDestruccion)
    {
        //throw new System.NotImplementedException();
    }

    public override void Atacar()
    {
        anim.SetTrigger("atacar"); //Lanzo desde aquí para que vaya enfocando al jugador
        Debug.Log("Lanzo");
    }


    private void EnfocarDestino()
    {
        //Para orientar al personaje hacia el destino
        if (target.transform.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one; //(1, 1, 1)
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
