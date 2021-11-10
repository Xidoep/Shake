using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake_Colision : MonoBehaviour
{
    [SerializeField] Shake_Camera impuls;
    [SerializeField] float gain;
    private void OnCollisionEnter(Collision collision)
    {
        impuls.Shake(collision.contacts[0].point, collision.impulse.magnitude * gain);
    }
}
