using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murcielago : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float velocidadPatrulla;
    [SerializeField] private float danoAtaque;
    private Vector3 destinoActual;
    private int indiceActual = 0;
    // Start is called before the first frame update
    void Start()
    {
        destinoActual = waypoints[indiceActual].position;
        StartCoroutine(Patrulla());
        //Lo hago tipo corrutina para que no se siga moviendo al llegar al destino
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Patrulla()
    {
        while (true)
        {
            while (transform.position != destinoActual)
            {
                //Espacio recorrido por unidad de tiempo = velocidad * tiempo
                transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidadPatrulla * Time.deltaTime);
                yield return null; //Vuelve lo más rápido posible para evitar cortes
            }
            //Cuando llego salgo del bucle y cambio el destino
            NuevoDestino();
        }

    }

    private void NuevoDestino()
    {
        indiceActual++;
        if (indiceActual >= waypoints.Length)
        {
            indiceActual = 0;
        }
        destinoActual = waypoints[indiceActual].position;
        EnfocarDestino();
    }

    private void EnfocarDestino()
    {
        //Para orientar al personaje hacia el destino
        if (destinoActual.x > transform.position.x)
        {
            transform.localScale = Vector3.one; //(1, 1, 1)
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
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
}
