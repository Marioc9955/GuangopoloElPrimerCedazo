   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorNivel5 : MonoBehaviour
{
    public bool conMunicion;

    int vida;

    GameObject GameControler;

    // Start is called before the first frame update
    void Start()
    {
        conMunicion = false;
        vida = 300;
        GameControler = GameObject.Find("GameController");
    }

    public void RestarVida()
    {
        vida--;
        GameControler.GetComponent<VidaJugadorUI>().ActualizarVida(vida);
    }
}
