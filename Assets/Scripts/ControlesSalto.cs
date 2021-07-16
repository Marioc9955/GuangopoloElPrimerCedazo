using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesSalto : MonoBehaviour
{
    Rigidbody2D rbPer;

    public float fuerzaSalto;

    bool saltando;

    void Start()
    {
        rbPer = GetComponent<Rigidbody2D>();
        saltando = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !saltando)
        {
            saltando = true;
            rbPer.AddForce(new Vector2(0, fuerzaSalto));
        }

        if (Input.GetKeyUp(KeyCode.Space) && rbPer.velocity.y > 0)
        {
            rbPer.velocity = new Vector2(rbPer.velocity.x, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            saltando = false;
        }
    }






}
