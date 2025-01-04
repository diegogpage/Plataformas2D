using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransporte : MonoBehaviour
{

    [SerializeField] private Transform targetPoint;
    private bool isTeleporting = false;
    

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
        if (elOtro.gameObject.CompareTag("Player") && !isTeleporting)
        {
            isTeleporting = true;
            Teletransporte targetTeleport = targetPoint.GetComponent<Teletransporte>();
            targetTeleport.isTeleporting = true;
            //Esto lo hago para que cuando llegue al otro punto del TP, la variable "isTeleporting" sea true.
            //De esta forma, cuando se teletransporte no entrara en el if y evito entrar en un bucle.
            //Hasta que no me separe del TP no se pone a false (OnTriggerExit) y, por tanto, no podré volver a teletransportarme

            elOtro.transform.position = targetPoint.position;
        }
    }

    private void OnTriggerExit2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
            isTeleporting = false;
        }
    }
}
