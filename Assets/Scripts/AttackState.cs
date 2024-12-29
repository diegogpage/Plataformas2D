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
        
        timer = timeBetweenAttacks; //Para que ataque nada más entrar
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenAttacks)
        {
            //Debug.Log("Te ataco");
            enemigo.Atacar();
            timer = 0f;
        }

        if (Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            controller.ChangeState(controller.ChaseState);
        }
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

}
