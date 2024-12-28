using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State<EnemyController>
{
    private Transform target;
    [SerializeField] private float chaseVelocity;
    [SerializeField] private float stoppingDistance;
    public override void OnEnterState(EnemyController controlador)
    {
        base.OnEnterState(controlador);
        Debug.Log("Persigo");
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.TryGetComponent(out Player player)) //Establezco quien es mi objetivo
        {
            target = player.transform;
        }
    }

    public override void OnUpdateState()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, chaseVelocity * Time.deltaTime);
        
        //Si me acerco hasta stopping distance cambio de estado
        if (Vector3.Distance(transform.position, target.position) <= stoppingDistance)
        {
            controller.ChangeState(controller.AttackState);
        }
    }

    public override void OnExitState()
    {
        
    }

    private void OnTriggerExit2D(Collider2D elOtro)
    {
        if (elOtro.TryGetComponent(out Player player)) //Establezco quien es mi objetivo
        {
            controller.ChangeState(controller.PatrolState);
        }
    }
}
