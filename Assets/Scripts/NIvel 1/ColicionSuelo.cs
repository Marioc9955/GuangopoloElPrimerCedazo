using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColicionSuelo : MonoBehaviour
{

    private ControlesNivel1 player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInChildren<ControlesNivel1> ();  
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "suelo" || collision.gameObject.tag == "obstaculo")
        {
            player.colicionSuelo = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "suelo" || collision.gameObject.tag == "obstaculo")
        {
            player.colicionSuelo = false;
        }
    }
}
