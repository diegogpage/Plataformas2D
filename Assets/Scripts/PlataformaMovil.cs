using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{ 
    [SerializeField] private Transform route;
    [SerializeField] private float velocity;

    private List<Vector3> listadoPuntos = new List<Vector3>();
    private Vector3 currentDestination;
    private int currentDestinationIndex;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform j in route)
        {
            //Recorro route y añado la posicion de cada punto al listado
            listadoPuntos.Add(j.position);
        }

        currentDestination = listadoPuntos[currentDestinationIndex];

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentDestination, velocity * Time.deltaTime);

        if (transform.position == currentDestination)
        {
            CalculateNewDestination();
        }
    }

    private void CalculateNewDestination()
    {
        currentDestinationIndex++;

        if (currentDestinationIndex >= listadoPuntos.Count)
        {
            currentDestinationIndex = 0;
        }

        currentDestination = listadoPuntos[currentDestinationIndex];
    }

    //Para que el jugador se mueva con la plataforma
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.transform.SetParent(transform); 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null); 
        }
    }
}
