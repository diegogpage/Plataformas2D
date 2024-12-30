using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State<EnemyController>
{
    private Transform target;
    [SerializeField] private float chaseVelocity;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private Enemigo enemigo;

    private Animator anim;
    private bool isBat;


    public override void OnEnterState(EnemyController controlador)
    {
        base.OnEnterState(controlador);
        anim = GetComponent<Animator>();
        enemigo.Perseguir();

        //Compruebo si es bat o no para congelar movimiento en Y
        isBat = GetComponent<Bat>() != null;
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
        if (isBat)
        {
            MovimientoLibre();
        }
        else
        {
            MovimientoEnX();
        }

        EnfocarDestino();

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

    private void EnfocarDestino()
    {
        //Para orientar al personaje hacia el destino
        if (target.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one; //(1, 1, 1)
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void MovimientoLibre() //Para el murcielago
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, chaseVelocity * Time.deltaTime);
    }

    private void MovimientoEnX() //Para el slime
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseVelocity * Time.deltaTime);
    }
}
