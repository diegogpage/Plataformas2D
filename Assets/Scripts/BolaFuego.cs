using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BolaFuego : MonoBehaviour
{
    [SerializeField] private float impulsoDisparo;
    private Rigidbody2D rb;
    private float timer;

    //Creo la pool y hago que se pueda acceder desde otro script
    //desde este accedo con myPool y desde el otro con MyPool
    private ObjectPool<BolaFuego> myPoolBolas;

    public ObjectPool<BolaFuego> MyPoolBolas { get => myPoolBolas; set => myPoolBolas = value; }


    //Uso OnEnable para que ocurra siempre que se active. Si lo dejo en start al reactivarlo no se le aplicaria fuerza
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero; // Limpia cualquier velocidad residual
        rb.angularVelocity = 0f;    // Limpia cualquier rotación previa
        //transform.forward --> mi eje z (azul)
        //transform.up --> mi eje y (verde)
        //transform.right --> mi eje x (rojo)
        //De esta forma si el mago está rotado y apunta hacia la izquierda, la bola se mueve hacia la izquierda
        rb.AddForce(transform.right * impulsoDisparo, ForceMode2D.Impulse);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 4)
        {
            myPoolBolas.Release(this); //Aqui uso myPool porque estoy en este script
            timer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
            myPoolBolas.Release(this);
        }
    }

}
