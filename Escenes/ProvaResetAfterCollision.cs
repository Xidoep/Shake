using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaResetAfterCollision : MonoBehaviour
{
    bool corrutineStarted = false;
    private Vector3 posicioInicial;

    // Start is called before the first frame update
    void Start()
    {
        posicioInicial = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (corrutineStarted)
            return;

        StartCoroutine(ResetPosition());

    }
    IEnumerator ResetPosition()
    {
        corrutineStarted = true;
        yield return new WaitForSeconds(1);
        transform.position = posicioInicial;
        corrutineStarted = false;
    }
}
