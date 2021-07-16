using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeNivel5 : MonoBehaviour
{
    public GameObject piedraDisparo;
    public Transform puntoDisparo;

    public float fuerzaDisparo;

    public Collider2D techoColl;

    int vida = 5;

    private void Start()
    {
        StartCoroutine(AtaqueLoop());
        StartCoroutine(AtaqueDesdeArriba());
    }

    IEnumerator AtaqueLoop()
    {
        while (true)
        {
            //ocultar el Jefe
            transform.position = new Vector3(20, transform.position.y, transform.position.z);

            //esperar para aparecer
            yield return new WaitForSeconds(1.5f);
            //aparecer izquiera o derecha o ataque desde arriba
            
            bool izquierda = Random.value > 0.5f;
            if (izquierda)
            {
                transform.position = new Vector3(-9, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(3, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.position = new Vector3(9, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(-3, transform.localScale.y, transform.localScale.z);
            }
            //espera para atacar
            yield return new WaitForSeconds(Random.value * 2);
            //atacar
            GameObject disparo = Instantiate(piedraDisparo, puntoDisparo.position, Quaternion.identity);
            Physics2D.IgnoreCollision(disparo.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            StartCoroutine(ActivarColisionEnUnSeg(disparo.GetComponent<Collider2D>(), GetComponent<Collider2D>()));
            disparo.GetComponent<Rigidbody2D>().AddForce(new Vector2(izquierda ? fuerzaDisparo : -fuerzaDisparo, 0));
            //espera para ocultarse
            yield return new WaitForSeconds(Random.value * 2);
        }
    }

    IEnumerator AtaqueDesdeArriba()
    {
        while (true)
        {
            bool ataqueLateral = Random.value < 0.420f;
            if (ataqueLateral)
            {
                //print("ataque desde arriba");
                int piedrasArrojar = Random.Range(5, 10);
                int piedrasArrojadas = 0;
                while (piedrasArrojadas < piedrasArrojar)
                {
                    GameObject piedra = Instantiate(piedraDisparo, new Vector3(Random.Range(-8f, 8f), 6, -0.5f), Quaternion.identity);
                    Physics2D.IgnoreCollision(piedra.GetComponent<Collider2D>(), techoColl);
                    StartCoroutine(ActivarColisionEnUnSeg(piedra.GetComponent<Collider2D>(), techoColl));
                    yield return new WaitForSeconds(Random.value);
                    piedrasArrojadas++;
                }
            }
            yield return new WaitForSeconds(Random.value * 5);
        }
        
    }

    IEnumerator ActivarColisionEnUnSeg(Collider2D c, Collider2D d)
    {
        yield return new WaitForSeconds(1);
        if (c != null && d != null)
        {
            Physics2D.IgnoreCollision(c, d, false);
        }
        
    }

    public IEnumerator EsAtacado()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0.2f, 0.2f);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        vida--;
        if (vida <= 0)
        {
            //fin del nivel
            print("Ganas");
            //Time.timeScale = 0;
        }
    }
}
