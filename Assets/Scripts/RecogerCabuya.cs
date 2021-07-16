using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecogerCabuya : MonoBehaviour
{
    public Text cantidadCabuyaTxt;

    int cantidadCabuya;

    private void Start()
    {
        cantidadCabuya = 0;
        //cantidadCabuyaTxt.text = "Cabuya recogida: " + cantidadCabuya;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("cabuya"))
        {
            cantidadCabuya++;
            Destroy(collision.gameObject);
            //cantidadCabuyaTxt.text = "Cabuya recogida: "+cantidadCabuya;
        }
    }
}
