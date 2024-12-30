using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemigo
{
    //[SerializeField] private Transform[] waypoints;
    //[SerializeField] private float velocidadPatrulla;
    [SerializeField] private float danoAtaque;

    private Animator anim;
    private bool atacando;
    //private Vector3 destinoActual;
    //private int indiceActual = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        atacando = false;
        //destinoActual = waypoints[indiceActual].position;
        //StartCoroutine(Patrulla());
        //Lo hago tipo corrutina para que no se siga moviendo al llegar al destino
    }

    // Update is called once per frame
    void Update()
    {

    }

    //IEnumerator Patrulla()
    //{
    //    while (true)
    //    {
    //        while (transform.position != destinoActual)
    //        {
    //            //Espacio recorrido por unidad de tiempo = velocidad * tiempo
    //            transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidadPatrulla * Time.deltaTime);
    //            yield return null; //Vuelve lo más rápido posible para evitar cortes
    //        }
    //        //Cuando llego salgo del bucle y cambio el destino
    //        NuevoDestino();
    //    }

    //}

    //private void NuevoDestino()
    //{
    //    indiceActual++;
    //    if (indiceActual >= waypoints.Length)
    //    {
    //        indiceActual = 0;
    //    }
    //    destinoActual = waypoints[indiceActual].position;
    //    EnfocarDestino(); 
    //}

    //private void EnfocarDestino()
    //{
    //    //Para orientar al personaje hacia el destino
    //    if(destinoActual.x > transform.position.x)
    //    {
    //        transform.localScale = Vector3.one; //(1, 1, 1)
    //    }
    //    else
    //    {
    //        transform.localScale = new Vector3(-1, 1, 1);
    //    }
    //}

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
            Debug.Log("toco");
        }
    }

    public override void Atacar()
    {
        Debug.Log("Ataco");
        atacando = true;
        anim.SetBool("atacando", true);

    }

    public override void Perseguir()
    {
        Debug.Log("Persigo");
        //atacando = false;
        anim.SetBool("atacando", false);
        //anim.SetBool("sorpresa", true);
    }

    public override void Morir(float tiempoDestruccion)
    {
        tiempoDestruccion = 0.1f;
        base.Morir(tiempoDestruccion);
        //anim.SetTrigger("explosion");
    }
}
