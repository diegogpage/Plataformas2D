using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private ParticleSystem particulas;

    // Start is called before the first frame update
    void Start()
    {
        particulas = GetComponentInChildren<ParticleSystem>();
        
        if (particulas != null)
        {
            particulas.Stop();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
            if (particulas != null)
            {
                Debug.Log("Checkpoint cogido");
                particulas.Play();
            }
        }
    }
}
