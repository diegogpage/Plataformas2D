using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaTemporal : MonoBehaviour
{
    private float timer;
    private float tiempoTransicion;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        tiempoTransicion = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= tiempoTransicion)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
