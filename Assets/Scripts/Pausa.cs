using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour
{
    bool pausa = false;

    public GameObject pantallaPausa;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausa)
            {
                pausa = true;
                pantallaPausa.SetActive(pausa);
                Time.timeScale = 0;
            }
            else
            {
                pausa = false;
                pantallaPausa.SetActive(pausa);
                Time.timeScale = 1;
            }

        }
    }
}
