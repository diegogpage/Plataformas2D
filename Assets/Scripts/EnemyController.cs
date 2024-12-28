using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PatrolState patrolState;
    private ChaseState chaseState;
    private AttackState attackState;

    private State<EnemyController> currentState;
    //Tipo state porque todos los estados heredan de state
    //Especifico que tipo de controller es en este caso T

    //Las encapsulo para poder acceder a ellas desde los distintos estados. Quito los set, no hacen falta
    public PatrolState PatrolState { get => patrolState;}
    public ChaseState ChaseState { get => chaseState;}
    public AttackState AttackState { get => attackState;}

    

    // Start is called before the first frame update
    void Start()
    {
        patrolState = GetComponent<PatrolState>();
        chaseState = GetComponent<ChaseState>();
        attackState = GetComponent<AttackState>();

        ChangeState(patrolState); //Sería lo mismo que poner currentState = patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        //Si existe un currentState, actualizalo
        if (currentState) 
        {
            currentState.OnUpdateState();
        }
    }

    public void ChangeState(State<EnemyController> newState)
    {
        //Para cambiar de estado salgo del que estaba, cambio a otro y entro en ese nuevo estado
        if (currentState)
        {
            currentState.OnExitState();
        }
        currentState = newState;
        currentState.OnEnterState(this); //EnemyController es el propio controlador
    }
}
