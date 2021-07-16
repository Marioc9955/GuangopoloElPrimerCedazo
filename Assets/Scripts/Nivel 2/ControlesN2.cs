using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesN2 : MonoBehaviour
{
    public GameObject camara;
    public float offsetCamara;
    
    public HingeJoint2D brazoDerHJ, brazoIzqHJ;

    public ControlManoNivel2[] manos;

    public bool estaAgarrado, agarradoConDosManos;

    public float velAngMax;

    float horizontalMov;

    Rigidbody2D rbPer;

    [SerializeField]
    private float fuerzaBalanceo;

    public GameObject manoAgarrada { get; set; }

    float anguloRotacion;

    void Start()
    {
        rbPer = GetComponent<Rigidbody2D>();
        estaAgarrado = agarradoConDosManos = false;
    }

    void Update()
    {
        if (estaAgarrado)
        {
            horizontalMov = Input.GetAxis("Horizontal");

            if (rbPer.angularVelocity <= velAngMax && rbPer.angularVelocity >= -velAngMax)
            {
                rbPer.AddForce(new Vector2(fuerzaBalanceo * horizontalMov, 0));
            }

            // Agarrarse con dos manos, aun no completo
            anguloRotacion = Quaternion.Angle(Quaternion.identity, transform.rotation);
            if (anguloRotacion > 50)
            {
                //doblar el brazo para abajo cuando se puede agarrar con las dos manos
                JointAngleLimits2D jl;
                switch (manoAgarrada.GetComponent<ControlManoNivel2>().mano)
                {
                    case Mano.Derecha:
                        jl = brazoIzqHJ.limits;
                        jl.min = -90;
                        brazoIzqHJ.limits = jl;

                        break;
                    case Mano.Izquierda:
                        jl = brazoDerHJ.limits;
                        jl.max = 90;
                        brazoDerHJ.limits = jl;

                        break;
                }
                manoAgarrada.GetComponent<ControlManoNivel2>().otraMano.GetComponent<ControlManoNivel2>().sePuedeAgarrar = false;

                if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.C))
                {
                    AgarrarseConDosManos();
                }
            }
            else
            {
                JointAngleLimits2D jl;
                switch (manoAgarrada.GetComponent<ControlManoNivel2>().mano)
                {
                    case Mano.Derecha:
                        jl = brazoIzqHJ.limits;

                        if (jl.min < 0)
                        {
                            jl.min += Time.deltaTime * 200;
                        }
                        brazoIzqHJ.limits = jl;

                        break;
                    case Mano.Izquierda:
                        jl = brazoDerHJ.limits;
                        if (jl.max > 0)
                        {
                            jl.max -= Time.deltaTime * 200;
                        }
                        brazoDerHJ.limits = jl;

                        break;
                }
                manoAgarrada.GetComponent<ControlManoNivel2>().otraMano.GetComponent<ControlManoNivel2>().sePuedeAgarrar = true;
            }
        }

        camara.transform.position = new Vector3(camara.transform.position.x, transform.position.y + offsetCamara, camara.transform.position.z);
    }

    void AgarrarseConDosManos()
    {   
        JointMotor2D jm2d = brazoDerHJ.motor;
        jm2d.motorSpeed = -200;
        brazoDerHJ.motor = jm2d;

        jm2d = brazoIzqHJ.motor;
        jm2d.motorSpeed = 200;
        brazoIzqHJ.motor = jm2d;

        StartCoroutine(AgarrarseConOtraMano());

        agarradoConDosManos = true;
    }

    IEnumerator AgarrarseConOtraMano()
    {
        yield return new WaitUntil(() => anguloRotacion < 5f);
        manoAgarrada.GetComponent<ControlManoNivel2>().AgarrarseConOtraMano();
    }

    public void SueltaUnaMano(GameObject manoAgarrada)
    {
        this.manoAgarrada = manoAgarrada;
        agarradoConDosManos = false;

        JointMotor2D jm2d = brazoDerHJ.motor;
        jm2d.motorSpeed = 200;
        brazoDerHJ.motor = jm2d;

        jm2d = brazoIzqHJ.motor;
        jm2d.motorSpeed = -200;
        brazoIzqHJ.motor = jm2d;
    }

    void ExtenderBrazoDerechoArriba()
    {
        JointMotor2D jm = brazoDerHJ.motor;
        jm.motorSpeed = -200;
        brazoDerHJ.motor = jm;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            ExtenderBrazoDerechoArriba();
            rbPer.freezeRotation = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            foreach (ControlManoNivel2 mano in manos)
            {
                mano.DesactivarLimitesYMotor();
            }

            JointMotor2D jm = brazoDerHJ.motor;
            jm.motorSpeed = 200;
            brazoDerHJ.motor = jm;

            jm = brazoIzqHJ.motor;
            jm.motorSpeed = -200;
            brazoIzqHJ.motor = jm;

            transform.rotation = Quaternion.identity;

            rbPer.freezeRotation = true;

            estaAgarrado = agarradoConDosManos = false;

            JointAngleLimits2D jl;
            jl = brazoIzqHJ.limits;
            jl.min = 0;
            brazoIzqHJ.limits = jl;

            jl = brazoDerHJ.limits;
            jl.max = 0;
            brazoDerHJ.limits = jl;

            if (manoAgarrada != null)
            {
                manoAgarrada.GetComponent<ControlManoNivel2>().sePuedeAgarrar = true;
                manoAgarrada.GetComponent<ControlManoNivel2>().otraMano.GetComponent<ControlManoNivel2>().sePuedeAgarrar = true;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("picos"))
        {
            Caer();
        }
    }

    public void Caer()
    {
        JointAngleLimits2D jl;
        jl = brazoIzqHJ.limits;
        jl.min = -90;
        brazoIzqHJ.limits = jl;

        jl = brazoDerHJ.limits;
        jl.max = 90;
        brazoDerHJ.limits = jl;

        JointMotor2D jm = brazoDerHJ.motor;
        jm.motorSpeed = 200;
        brazoDerHJ.motor = jm;

        jm = brazoIzqHJ.motor;
        jm.motorSpeed = -200;
        brazoIzqHJ.motor = jm;

        ControlManoNivel2[] manos = transform.GetComponentsInChildren<ControlManoNivel2>();
        foreach (var mano in manos)
        {
            mano.SoltarYCaer();
        }
        
    }  

    public void SaltoDeLado(Vector2 direccion)
    {
        rbPer.AddForce(direccion *  520f);
        print("saltó");
    }
}
