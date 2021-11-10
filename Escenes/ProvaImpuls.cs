using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ProvaImpuls : MonoBehaviour
{
    public Shake_Camera impuls;
    public float forca;

    [ContextMenu("Impulse")]
    public void Impulse()
    {
        impuls.Shake(transform.position, forca);
        //impuls.Shake(Camera.main.transform, 10, 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Impulse();
    }
}
