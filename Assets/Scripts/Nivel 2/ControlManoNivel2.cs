using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Mano { Derecha, Izquierda}

public class ControlManoNivel2 : MonoBehaviour
{
    public GameObject personaje, ejeBD, ejeBI;

    public Mano mano;

    public GameObject otraMano;

    FixedJoint2D fj2d;

    HingeJoint2D hj2d;

    public bool sePuedeAgarrar;

    KeyCode kcAgarrarse;

    bool enPiedra;

    PiedraInestable pi;

    private void Start()
    {
        fj2d = gameObject.GetComponent<FixedJoint2D>();
        hj2d = gameObject.GetComponent<HingeJoint2D>();
        fj2d.enabled = false;
        sePuedeAgarrar = true;
        enPiedra = false;

        switch (mano)
        {
            case Mano.Derecha:
                kcAgarrarse = KeyCode.C;
                break;
            case Mano.Izquierda:
                kcAgarrarse = KeyCode.Z;
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(kcAgarrarse))
        {

            if (personaje.GetComponent<ControlesN2>().agarradoConDosManos)
            {
                DesactivarLimitesYMotor();
                otraMano.GetComponent<ControlManoNivel2>().DesactivarLimitesYMotor();

                personaje.GetComponent<ControlesN2>().SueltaUnaMano(otraMano);
            }
            else if (fj2d.enabled && !otraMano.GetComponent<FixedJoint2D>().enabled)
            {
                Vector2 direc = otraMano.transform.position - personaje.transform.position;
                personaje.GetComponent<ControlesN2>().SaltoDeLado(direc);
            }

            fj2d.enabled = false;

            if (otraMano.GetComponent<FixedJoint2D>().enabled)
            {
                personaje.GetComponent<ControlesN2>().manoAgarrada = otraMano;
            }
            else
            {
                personaje.GetComponent<ControlesN2>().estaAgarrado = false;
            }
        }

        //agarrarse 
        //if (enPiedra && Keyboard.current[kAga].wasPressedThisFrame)
        if (enPiedra && Input.GetKeyDown(kcAgarrarse))
        {
            fj2d.enabled = true;

            JointMotor2D jm2d = ejeBD.GetComponent<HingeJoint2D>().motor;
            jm2d.motorSpeed = 200;
            ejeBD.GetComponent<HingeJoint2D>().motor = jm2d;

            personaje.GetComponent<ControlesN2>().estaAgarrado = true;

            personaje.GetComponent<ControlesN2>().manoAgarrada = gameObject;

            if (pi != null)
            {
                StartCoroutine(pi.RomperPiedra(this));
            }
        }
    }

    public void AgarrarseConOtraMano()
    {
        if (sePuedeAgarrar)
        {
            otraMano.GetComponent<FixedJoint2D>().enabled = true;
        }
        
    }

    public void DesactivarLimitesYMotor()
    {
        hj2d.useLimits = false;
        hj2d.useMotor = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("piedra"))
        {
            if (sePuedeAgarrar)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                enPiedra = true;

                collision.gameObject.TryGetComponent(out pi);
            }
            
        }

        if (collision.gameObject.CompareTag("picos"))
        {
            personaje.GetComponent<ControlesN2>().Caer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("piedra"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            enPiedra = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("piedra") && sePuedeAgarrar)
        {
           enPiedra = true;
        }
    }

    public void SoltarYCaer()
    {
        sePuedeAgarrar = false;
        fj2d.enabled = false;

    }
}
