using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MecanicasN4 : MonoBehaviour
{
    public float tiempoCaida;


    // Start is called before the first frame update
    void Start()
    {
        tiempoCaida = 2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("plataformaRompible"))
        {
            StartCoroutine(RomperPlataforma(collision.gameObject));
        }

        if (collision.gameObject.CompareTag("picos"))
        {
            ReiniciarNivel();
        }
    }

    IEnumerator RomperPlataforma(GameObject plataforma)
    {
        yield return new WaitForSecondsRealtime(tiempoCaida);
        Destroy(plataforma);
    }

    void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
