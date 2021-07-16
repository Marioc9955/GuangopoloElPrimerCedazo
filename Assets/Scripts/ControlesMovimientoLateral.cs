using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesMovimientoLateral : MonoBehaviour
{

    Rigidbody2D rbPer;

    public float fuerzaInicial, velocidadLimite;

    float horizontalMov;

    bool puedeMoverse;

    public bool puedeGirar;

    void Start()
    {
        rbPer = GetComponent<Rigidbody2D>();
        puedeMoverse = true;
        puedeGirar = true;
    }

    
    void Update()
    {
        horizontalMov = Input.GetAxis("Horizontal");

        if (puedeGirar)
        {
            if (horizontalMov > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontalMov < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (rbPer.velocity.magnitude <= velocidadLimite && rbPer.velocity.magnitude >= -velocidadLimite && puedeMoverse)
        {
            rbPer.AddForce(new Vector2(fuerzaInicial * horizontalMov, 0));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            puedeMoverse = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            puedeMoverse = true;
        }
    }
}
