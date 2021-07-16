using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesDisparoNivel5 : MonoBehaviour
{
	public Transform pivotCedazo;
	public float springRange;
	public float fuerzaDisparo;

	Rigidbody2D rb;

	Transform playerTrans;

	GameObject player;

	int vida = 2;

	public GameObject particlesDestruccion;
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerTrans = player.transform;

		springRange = 0.5f;
		fuerzaDisparo = 420;
		pivotCedazo = GameObject.Find("cedazo").transform;
	}

	bool sePuedeLanzar = false;
	Vector3 dis;
	void OnMouseDrag()
	{
        if (!sePuedeLanzar)
        {
			return;
        }
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		dis = pos - pivotCedazo.position;
		player.GetComponent<ControlesMovimientoLateral>().puedeGirar = false;
        if (dis.x > 0.25f)
        {
			playerTrans.localScale = new Vector3(-1, 1, 1);
        }
        else if (dis.x < 0.25f)
		{
			playerTrans.localScale = new Vector3(1, 1, 1);
		}

		dis.z = 0;
		if (dis.magnitude > springRange)
		{
			dis = dis.normalized * springRange;
		}
		transform.position = dis + pivotCedazo.position - new Vector3(0, 0, 0.3f);
	}

	void OnMouseUp()
	{
		if (!sePuedeLanzar)
		{
			return;
		}
		player.GetComponent<ControlesMovimientoLateral>().puedeGirar = true;
		sePuedeLanzar = false;
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.AddForce(-dis * fuerzaDisparo);
		transform.parent = null;
		player.GetComponent<JugadorNivel5>().conMunicion = false;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		switch (collision.gameObject.tag)
		{
			case "Player":
				if (!player.GetComponent<JugadorNivel5>().conMunicion)
				{
					ColocarEnCedazo();
				}
				break;
			case "jefe":
				StartCoroutine(collision.gameObject.GetComponent<JefeNivel5>().EsAtacado());
				break;
			default:
                if (vida == 0)
                {
					Destroy(gameObject);
					Instantiate(particlesDestruccion, transform.position, Quaternion.identity);
				}
				vida--;
				break;
		}
    }

	public void ColocarEnCedazo()
    {
		transform.position = pivotCedazo.position - new Vector3(0, 0, 0.3f);
		transform.parent = pivotCedazo;
		rb.bodyType = RigidbodyType2D.Kinematic;
		sePuedeLanzar = true;
		rb.velocity = Vector3.zero;
		player.GetComponent<JugadorNivel5>().conMunicion = true;
	}
}
