using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CedazoAtrapaPiedras : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("piedra"))
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && !player.GetComponent<JugadorNivel5>().conMunicion)
            {
                if (collision.gameObject.TryGetComponent(out DisparoJefe dj))
                {
                    dj.EsAtrapada();
                }
                else if (collision.gameObject.TryGetComponent(out ControlesDisparoNivel5 cd))
                {
                    cd.ColocarEnCedazo();
                }
                
            }
        }
    }
}
