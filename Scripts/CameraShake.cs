using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraShake : MonoBehaviour {

    #region Instance
    public static CameraShake Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    public bool shake;

    public float temps;
    public float forca;
    public bool vibrar;
    public bool flash;
    public Color color;
    public float noiseBias;

    float tempsTotal;
    float tempsActual;
    float forcaTotal;
    float forcaActual;
    float flashActual;
    public bool vibrarActual;

    public Camera cam;
    public GameObject flashCanvas;
    public CameraFlash cameraFlash;

    Coroutine tremolor;


    private void Update()
    {
        if (!shake)
            return;

    }



    public void Shake(float _temps, float _forca, bool _vibrar, bool _flash, Color _color)//temps/força,vibrar,flash,zoom
    {
        temps = _temps;
        forca = _forca;
        vibrar = _vibrar;
        flash = _flash;
        color = _color;
        Shake();
    }
    public void Shake(float _temps, float _forca, bool _flash, Color _color)//temps/força,flash,zoom
    {
        temps = _temps;
        forca = _forca;
        flash = _flash;
        color = _color;
        Shake();
    }
    public void Shake(bool _vibrar, bool _flash, Color _color)//vibrar,flash
    {
        vibrar = _vibrar;
        flash = _flash;
        color = _color;
        Shake();
    }
    public void Shake(bool _flash, Color _color)//flash,zoom
    {
        flash = _flash;
        color = _color;
        Shake();
    }
    public void Shake(bool _vibrar)//vibrar,zoom
    {
        vibrar = _vibrar;
        Shake();
    }
    public void Shake(float _temps, float _forca)//temps/força,zoom
    {
        temps = _temps;
        forca = _forca;
        Shake();
    }
    public void Shake(float _forca)
    {
        if (forca == _forca)
            return;

        temps = 0;
        forca = _forca;
        Shake();
    }

    [ContextMenu("Shake")]
    public void Shake()
    {
        if(temps > 0)
        {
            if(tempsActual > 10)
            {
                tempsTotal = 0;
                tempsActual = 0;
            }
            tempsTotal += temps;
            tempsActual += temps;
            forcaTotal += forca;
        }
        else
        {
            tempsTotal = 1000;
            tempsActual = 1000;
            forcaTotal = forca;
        }
        
        //tempsActual += temps;
        noiseBias = Random.Range(-100, 100);
        if (flash) flashCanvas.SetActive(false);
        flashActual = 0;
        vibrarActual = vibrar;

        if(forcaTotal > 0)
        {
            shake = true;
            if (tremolor == null) tremolor = StartCoroutine(Tremolor());
        }
        else
        {
            shake = false;
            StopCoroutine(tremolor);
            tremolor = null;
            if (transform.localPosition != Vector3.zero) transform.localPosition = Vector3.zero;
            if (transform.localEulerAngles != Vector3.zero) transform.localEulerAngles = Vector3.zero;
        }
    }
    
    float Aleatori(float _forca, float _extraBias)
    {
        return ((Mathf.PerlinNoise((tempsActual * 15) + noiseBias + _extraBias, -(tempsActual * 10) + noiseBias - _extraBias) * 2f) - 1) * _forca;
    }

    void ShakeEfecte() {
        if(tempsTotal > 0)
        {
            forcaActual = (tempsActual/tempsTotal) * forcaTotal;
        }
        else
        {
            forcaActual = forcaTotal;
        }

        float _x = Aleatori(forcaActual,0);
        float _y = Aleatori(forcaActual,1);
        float _z = Aleatori(forcaActual,2);

        transform.localRotation = Quaternion.Euler(_x, _y, _z);
        transform.localPosition = new Vector3(_x / 10f, _y / 10f, _z / 10f);
        if(tempsActual > 0)
        {
            tempsActual -= Time.deltaTime;
        }

        if (vibrarActual)
        {
            if (forcaActual > (forca * 0.75f))
            {
                float impulseMagnitude = Mathf.Abs(forcaTotal);
                if(impulseMagnitude > 0)
                {
                    Gamepad.current.SetMotorSpeeds(impulseMagnitude,impulseMagnitude);
                }
                else
                {
                    Gamepad.current.SetMotorSpeeds(0, 0);
                }
            }
        }
        else
        {
            vibrarActual = false;
        }

        if (flash)
        {
            if (flashActual < 0.2f)
            {
                if (!flashCanvas.activeSelf)
                {
                    flashCanvas.SetActive(true);

                    if(cameraFlash == null)
                        cameraFlash = flashCanvas.GetComponent<CameraFlash>();
                    
                    cameraFlash.flash.color = color;
                }
                flashActual += Time.deltaTime;
            }
            else
            {
                if (flashCanvas.activeSelf)
                {
                    flashCanvas.SetActive(false);
                }
            }
        }

        if (tempsActual < 0) {
            shake = false;
            tempsTotal = 0;
            tempsActual = 0;
            forcaTotal = 0;
            forcaActual = 0;
            flashActual = 0;
            vibrarActual = false;
            transform.localPosition = new Vector3(0, 0, 0);
        }

    }

    IEnumerator Tremolor()
    {
        while(tempsActual > 0)
        {
            ShakeEfecte();
            yield return null;
        }
        tremolor = null;
        yield return null;
    }
}
