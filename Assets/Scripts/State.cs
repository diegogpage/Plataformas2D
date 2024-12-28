using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> : MonoBehaviour
{
    //<T> indica que es gen�rico. Esto se hace por si hay varios controller (EnemyController, NPCController...)
    //En ese caso ponemos <T> y as� aceptar�a todos los controller

    //Desde cada estado necesito acceso al controlador. As� que se da desde aqu�
    protected T controller;
    public virtual void OnEnterState(T controlador)
    {
        this.controller = controlador;
    }
    //Con que lo haga en el OnEnterState es suficiente ya que todos los estados pasan por ah� primero

    public abstract void OnUpdateState();

    public abstract void OnExitState();

}
