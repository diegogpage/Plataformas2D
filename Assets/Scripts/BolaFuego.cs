using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFuego : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float impulsoDisparo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //transform.forward --> mi eje z (azul)
        //transform.up --> mi eje y (verde)
        //transform.right --> mi eje x (rojo)
        //De esta forma si el mago está rotado y apunta hacia la izquierda, la bola se mueve hacia la izquierda
        rb.AddForce(transform.right * impulsoDisparo, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
