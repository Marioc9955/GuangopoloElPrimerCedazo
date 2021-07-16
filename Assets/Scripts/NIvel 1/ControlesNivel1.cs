using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlesNivel1 : MonoBehaviour
{
    private Rigidbody2D rbP;

    public float fuerzaInicial, fuerzaSalto, velocidadLimite, offsetCamara;

    public Camera camara;

    public bool colicionSuelo;

    bool corriendo,saltando, agachado ;

    // Start is called before the first frame update
    void Start()
    {
        rbP = GetComponent<Rigidbody2D>();
        corriendo = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimieto camara 
        camara.transform.position = new Vector3(transform.position.x + offsetCamara, transform.position.y, -10);
        
        #region deteccion de teclas 

        //Salto
        //Saltando 
        if (Input.GetKeyDown(KeyCode.Space) && colicionSuelo)
        {
            corriendo = false;
            saltando = true;
        }
        //Corriendo 
        if (Input.GetKeyUp(KeyCode.Space))
        {
            corriendo = true;

        }
        //Agacharse 

        //Agachado
        if (Input.GetKeyDown(KeyCode.DownArrow) && colicionSuelo)
        {
            agachado = true;
            corriendo = false;

        }
        //De pie 
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            corriendo = true;
            transform.rotation = Quaternion.identity;
        }
        #endregion

        #region muerteRenicio
        //reinicio  del nivel 
        if (transform.position.y <= -10)
        {
            ReiniciarNivel();
        }
        #endregion

    }


    private void FixedUpdate()
    {

        if (rbP.velocity.magnitude <= velocidadLimite && corriendo)
        {
            rbP.AddForce(new Vector2(fuerzaInicial, 0));
        }

        if (saltando)
        {
            rbP.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            saltando = false;
        }


        if (agachado)
        {
            transform.Rotate(0, 0, 70);
            agachado = false;
        }

        if (rbP.velocity.magnitude <= 0.5 && colicionSuelo && rbP.velocity.x == 0)
        {
            transform.rotation = Quaternion.identity;
            corriendo = true;
        }


    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstaculo"))
        {
            velocidadLimite = 5;
        }
        if (collision.gameObject.CompareTag("picos"))
        {
            ReiniciarNivel();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstaculo"))
        {
            velocidadLimite = 20;
        }
    }

    void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
