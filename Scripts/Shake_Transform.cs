using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake_Transform : MonoBehaviour
{
    [SerializeField] AnimationCurve intensity;
    [SerializeField] float time;
    [SerializeField] float frequence;
    [SerializeField] bool position;
    [SerializeField] bool rotation;

    Vector3 initialPosition;
    Vector3 initialRotation;
    bool active = false;
    float timer;



    float Timer => (timer / time) * frequence;
    float Instensity => intensity.Evaluate(timer / time);
    bool Ended => timer >= time;

    Vector3 tmpNoise;

    [ContextMenu("Start")]
    public void Start()
    {
        if (!active) 
        {
            if (position) initialPosition = transform.localPosition;
            if (rotation) initialRotation = transform.localEulerAngles;
        } 
        active = true;
        timer = 0;
    }

    private void Update()
    {
        if (!active)
            return;

        timer += Time.deltaTime;

        tmpNoise = new Vector3((Mathf.PerlinNoise(Timer - 1, Timer + 1) - 0.5f) * Instensity, (Mathf.PerlinNoise(Timer - 2, -Timer + 2) - 0.5f) * Instensity, (Mathf.PerlinNoise(Timer - 3, Timer + 3) - 0.5f) * Instensity);
        
        if (position) transform.localPosition = initialPosition + tmpNoise;
        if (rotation) transform.localEulerAngles = initialRotation + tmpNoise;

        if (Ended)
        {
            Stop();
        }
    }

    [ContextMenu("Stop")]
    public void Stop()
    {
        if (position) transform.localPosition = initialPosition;
        if (rotation) transform.localEulerAngles = initialRotation;
        active = false;
    }
}
