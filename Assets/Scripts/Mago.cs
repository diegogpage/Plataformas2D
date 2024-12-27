using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : MonoBehaviour
{

    [SerializeField] private GameObject bolaFuegoPrefab;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaque;
    [SerializeField] private float danhoAtaque;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RutinaAtaque());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RutinaAtaque()
    {
        while (true) //Bucle infinito
        {
            anim.SetTrigger("atacar"); //Pongo la animaci�n
            yield return new WaitForSeconds(tiempoAtaque);
        }
    }

    private void LanzarBola()
    {
        //Desde un evento en la animaci�n instancio las bolas
        Instantiate(bolaFuegoPrefab, puntoSpawn.position, transform.rotation);
        //Las saco con la rotaci�n del mago por si este mira a la izquierda o a la derecha
    }
}
