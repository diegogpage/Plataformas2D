using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Bat") || elOtro.gameObject.CompareTag("Slime") || elOtro.gameObject.CompareTag("Mago") || elOtro.gameObject.CompareTag("BolaFuego"))
        {
            Destroy(elOtro.gameObject);
            Debug.Log("Te protejo");
        }
    }
}
