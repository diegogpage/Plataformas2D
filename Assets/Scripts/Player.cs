using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputH;

    [SerializeField] private float vidaPlayer;
    [SerializeField] private GameObject[] cajas;
    private bool moviendo;

    [Header("Sistema de movimiento")]
    [SerializeField] private Transform posicionPies;
    [SerializeField] private float velocidad;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float distanciaAlSuelo;
    [SerializeField] private LayerMask queEsSaltable;

    [Header("Sistema de combate")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private LayerMask queEsDanhable;
    [SerializeField] private float danhoAtaque;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Uso rb porque es dynamic
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moviendo = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        Saltar();

        LanzarAtaque();

        Muerte();

        MoverCaja();
    }

    private void Muerte()
    {
        if (vidaPlayer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void LanzarAtaque()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }

    //Se lanza desde evento de animaci�n
    private void Ataque()
    {
        //Lanzar trigger instant�neo (para que no est� siempre presente)
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);

        //foreach funciona igual que el for. "Para cada item de collidersTocados..."
        //foreach siempre incrementa de 1 en 1 y el for puedes cambiarlos (i++, i += 2, ...)
        foreach (Collider2D item in collidersTocados)
        {
            SistemaVidas sistemaVidasEnemigos = item.gameObject.GetComponent<SistemaVidas>();
            sistemaVidasEnemigos.RecibirDano(danhoAtaque);

            //if (item.TryGetComponent(out BolaFuego bolaFuego))
            //{
            //    Debug.Log("Rompo la bola");
            //    Destroy(item.gameObject);
            //}
        }

    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstoyEnSuelo()) //Si no pongo nada quiere decir que estoy en suelo es true
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }

    private bool EstoyEnSuelo()
    {
        bool tocado = Physics2D.Raycast(posicionPies.position, Vector3.down, distanciaAlSuelo, queEsSaltable);
        return tocado;
        //Si toco el suelo devuelve true y si no false. As� quito el doble salto
    }

    private void Movimiento()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * velocidad, rb.velocity.y);
        //Multiplico solo en x para que no afecte a y. Adem�s pongo rb.velocity.y y no 0 para respetarla en y

        if (inputH != 0) //Hay movimiento
        {
            anim.SetBool("running", true);
            if (inputH > 0) //Hacia la derecha
            {
                transform.eulerAngles = Vector3.zero;
            }
            else //Hacia la izquierda
            {
                transform.eulerAngles = new Vector3(0, 180, 0); //Roto sobre Y para que gire el personaje
            }
        }
        else //No hay movimiento
        {
            anim.SetBool("running", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }

    public void quitarVidaPlayer(float danho)
    {
        vidaPlayer -= danho;
    }

    private void MoverCaja()
    {
        moviendo = Input.GetKey(KeyCode.E);
        //Rigidbody2D rb = cajas.gameObject.GetComponent<Rigidbody2D>();
        for (int i = 0; i < cajas.Length; i++)
        {
            Rigidbody2D rb = cajas[i].gameObject.GetComponent<Rigidbody2D>();

            if (moviendo)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("BolaFuego"))
        {
            quitarVidaPlayer(10);
        }

    }
}
