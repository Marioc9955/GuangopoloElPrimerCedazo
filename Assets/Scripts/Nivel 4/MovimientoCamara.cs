using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoCamara : MonoBehaviour
{

   float movimiento, velocidadMov, movimientoMax;

    public GameObject personaje;

    // Start is called before the first frame update
    void Start()
    {
        movimiento = transform.position.y;
        velocidadMov = 5f;
        movimientoMax = -40;
    }

    // Update is called once per frame
    void Update()
    {
        if (movimiento >= movimientoMax)
        {
            movimiento -= velocidadMov * Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {

        transform.position = new Vector3(transform.position.x, movimiento, transform.position.z);
    }


}
