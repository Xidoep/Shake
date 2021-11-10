using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFlash : MonoBehaviour {

    public Image flash;

    private void OnEnable()
    {
        Iniciar(flash.color);
    }

    public void Iniciar(Color color)
    {
        flash.color = new Color(color.r, color.g, color.b, 1);
    }

    private void Update()
    {
        float _tmp = flash.color.a;
        if (_tmp > 0)
        {
            _tmp -= Time.deltaTime*10;
            flash.color = new Color(flash.color.r, flash.color.g, flash.color.b, _tmp);
        }
        else
        {
            flash.color = new Color(flash.color.r, flash.color.g, flash.color.b, 0);
            gameObject.SetActive(false);
        }
        
    }

}
