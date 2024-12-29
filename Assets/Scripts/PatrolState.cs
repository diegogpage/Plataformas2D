using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State<EnemyController>
{
    [SerializeField] private Transform route;
    [SerializeField] private float patrolVelocity;

    private List<Vector3> listadoPuntos = new List<Vector3>();

    private Vector3 currentDestination;
    private int currentDestinationIndex;
    private Animator anim;

    public override void OnEnterState(EnemyController controlador)
    {
        base.OnEnterState(controlador);
        anim = GetComponent<Animator>();

        foreach (Transform j in route)
        {
            //Recorro route y añado la posicion de cada punto al listado
            listadoPuntos.Add(j.position);
        }

        currentDestination = listadoPuntos[currentDestinationIndex];
        Debug.Log("Patrullo");

        //anim.SetBool("sorpresa", false);
    }

    public override void OnUpdateState()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentDestination, patrolVelocity * Time.deltaTime);
        //MoveTowards es para moverte hacia un objetivo

        if (transform.position == currentDestination)
        {
            CalculateNewDestination();
            EnfocarDestino();
        }
    }

    private void CalculateNewDestination()
    {
        //Cuando llego a un punto avanzo al siguiente de la lista. Si llego al final vuelvo al primero
        currentDestinationIndex++;

        if (currentDestinationIndex >= listadoPuntos.Count) 
        { 
            currentDestinationIndex = 0;
        }

        //Actualizo el destino
        currentDestination = listadoPuntos[currentDestinationIndex];
    }

    public override void OnExitState()
    {
        //Al salir limpio el listado de puntos para que si vuelvo a entrar no se dupliquen los puntos
        listadoPuntos.Clear();
        currentDestinationIndex = 0;
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if(elOtro.TryGetComponent(out Player player)) //Miro si tiene el script de player
        {
            controller.ChangeState(controller.ChaseState);
        }
    }

    private void EnfocarDestino()
    {
        //Para orientar al personaje hacia el destino
        if (currentDestination.x > transform.position.x)
        {
            transform.localScale = Vector3.one; //(1, 1, 1)
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
