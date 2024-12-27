using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputH;
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
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        Saltar();

        LanzarAtaque();
    }

    private void LanzarAtaque()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }

    //Se lanza desde evento de animación
    private void Ataque()
    {
        //Lanzar trigger instantáneo (para que no esté siempre presente)
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);

        //foreach funciona igual que el for. "Para cada item de collidersTocados..."
        //foreach siempre incrementa de 1 en 1 y el for puedes cambiarlos (i++, i += 2, ...)
        foreach (Collider2D item in collidersTocados)
        {
            SistemaVidas sistemaVidasEnemigos = item.gameObject.GetComponent<SistemaVidas>();
            sistemaVidasEnemigos.RecibirDano(danhoAtaque);
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
        //Si toco el suelo devuelve true y si no false. Así quito el doble salto
    }

    private void Movimiento()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * velocidad, rb.velocity.y);
        //Multiplico solo en x para que no afecte a y. Además pongo rb.velocity.y y no 0 para respetarla en y

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
}
