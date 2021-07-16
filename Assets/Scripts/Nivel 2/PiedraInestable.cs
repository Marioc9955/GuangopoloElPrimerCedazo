using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiedraInestable : MonoBehaviour
{
    public IEnumerator RomperPiedra(ControlManoNivel2 mano)
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
        mano.SoltarYCaer();
        mano.otraMano.GetComponent<ControlManoNivel2>().SoltarYCaer();
    }
}
