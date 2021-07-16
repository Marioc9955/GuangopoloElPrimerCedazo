using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJefe : MonoBehaviour
{
    public GameObject particlesDestruccion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "suelo":
                gameObject.AddComponent<ControlesDisparoNivel5>().particlesDestruccion = particlesDestruccion;
                Destroy(this);
                break;
            case "Player":
                collision.gameObject.GetComponent<JugadorNivel5>().RestarVida();
                break;
            default:
                Instantiate(particlesDestruccion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
        }
    }

    public void EsAtrapada()
    {
        ControlesDisparoNivel5 cd = gameObject.AddComponent<ControlesDisparoNivel5>();
        cd.particlesDestruccion = particlesDestruccion;
        cd.ColocarEnCedazo();
        Destroy(this);
    }
}
