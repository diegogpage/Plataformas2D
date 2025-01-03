using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<EnemyController>
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Enemigo enemigo;

    private float timer;
    private Transform target;
    private Animator anim;

    public override void OnEnterState(EnemyController controlador)
    {
        base.OnEnterState(controlador);
        anim = GetComponent<Animator>();
        
        timer = timeBetweenAttacks; //Para que ataque nada m�s entrar
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenAttacks)
        {
            enemigo.Atacar();
            timer = 0f;
        }

        if (Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            controller.ChangeState(controller.ChaseState);
        }
        EnfocarDestino();
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.TryGetComponent(out Player player)) //Establezco quien es mi objetivo
        {
            target = player.transform;
        }
    }

    public override void OnExitState()
    {
        
    }

    private void EnfocarDestino()
    {
        //Para orientar al personaje hacia el destino
        if (target.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
