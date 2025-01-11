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
    [SerializeField] private Player player;
    private bool moviendo;
    private float timer;
    private float timerEscudo;
    private bool defensa;
    private int estrella;
    [SerializeField] private Transform respawn;

    [Header("Sistema de movimiento")]
    [SerializeField] private Transform posicionPies;
    [SerializeField] private float velocidad;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float distanciaAlSuelo;
    [SerializeField] private LayerMask queEsSaltable;
    private bool resbaladizo;

    [Header("Sistema de combate")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private LayerMask queEsDanhable;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private Escudo escudo;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Uso rb porque es dynamic
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        //Caja
        moviendo = false;
        
        //Escudo
        escudo.gameObject.SetActive(false);
        timerEscudo = 5f;
        defensa = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        Saltar();

        LanzarAtaque();

        Muerte();

        MoverCaja();

        ActivarEscudo();
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
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && EstoyEnSuelo()) //Si no pongo nada quiere decir que estoy en suelo es true
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

        if (resbaladizo)
        {
            Vector2 targetVelocity = new Vector2(inputH * velocidad, rb.velocity.y);
            rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.deltaTime * 0.5f);

            if (inputH == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * 1, rb.velocity.y);
            }

        }
        else
        {
            rb.velocity = new Vector2(inputH * velocidad, rb.velocity.y);
            //Multiplico solo en x para que no afecte a y. Además pongo rb.velocity.y y no 0 para respetarla en y
        }


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

        else if (elOtro.gameObject.CompareTag("Hielo"))
        {
            resbaladizo = true;
        }
    }

    private void OnCollisionExit2D(Collision2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Hielo"))
        {
            resbaladizo = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Escudo"))
        {
            defensa = true;
            Destroy(elOtro.gameObject);
        }

        else if (elOtro.gameObject.CompareTag("Estrella"))
        {
            estrella++;
            Destroy(elOtro.gameObject);
        }

        else if (elOtro.gameObject.CompareTag("Muerte"))
        {
            player.transform.position = respawn.position;
        }

        else if (elOtro.gameObject.CompareTag("Checkpoint"))
        {
            respawn.position = elOtro.transform.position;
        }

        else if (elOtro.gameObject.CompareTag("Parar"))
        {
            Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void ActivarEscudo()
    {
        if (defensa)
        {
            escudo.gameObject.SetActive(true);

            timer += Time.deltaTime;
            if (timer >= timerEscudo)
            {
                escudo.gameObject.SetActive(false);
                defensa = false;
                timer = 0;
            }
        }
        
    }
}
