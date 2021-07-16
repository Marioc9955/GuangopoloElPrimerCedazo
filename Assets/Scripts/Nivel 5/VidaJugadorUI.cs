using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaJugadorUI : MonoBehaviour
{
    public GameObject vidaBarra, pantallaGameOver;

    Image[] vidas;

    private void Start()
    {
        vidas = new Image[vidaBarra.transform.childCount];
        for (int i = 0; i < vidas.Length; i++)
        {
            if (vidaBarra.transform.GetChild(i).gameObject.TryGetComponent<Image>(out Image im))
            {
                vidas[i] = im;
                vidas[i].color = new Color(1, 1, 1);
            }
            
        }
    }

    public void ActualizarVida(int vidasAct)
    {
        for (int i = 0; i < vidaBarra.transform.childCount; i++)
        {
            if (i < vidasAct)
            {
                vidas[i].color = new Color(1, 1, 1);
            }
            else
            {
                vidas[i].color = new Color(0.1f, 0.1f, 0.1f);
            }
        }
        if (vidasAct <= 0)
        {
            pantallaGameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
