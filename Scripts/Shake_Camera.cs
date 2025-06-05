using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

[CreateAssetMenu(menuName = "Xido Studio/Camera Shake/Impuls", fileName = "CameraImpuls")]
public class Shake_Camera : ScriptableObject
{
    static CinemachineImpulseSource impulseSource;

    [SerializeField] NoiseSettings noiseSettings;

    //public static CameraShake cameraShake;
    //public GameObject prefab_flash;

    public void Shake(Vector3 posicio, float forca = 1000)
    {
        if(impulseSource == null)
        {
            GameObject go = new GameObject("impulseSource", new System.Type[] { typeof(CinemachineImpulseSource) });
            impulseSource = go.GetComponent<CinemachineImpulseSource>();
        }

        impulseSource.ImpulseDefinition.RawSignal = noiseSettings;
        impulseSource.ImpulseDefinition.AmplitudeGain = forca;

        impulseSource.GenerateImpulseAt(posicio, Vector3.up);

    }

    /*public void Shake(Transform camara, float forca, float temps)
    {
        SetCameraShake(camara);

        cameraShake.forca = forca;
        cameraShake.temps = temps;
        cameraShake.flash = true;
        if(cameraShake.flashCanvas == null)
        {
            cameraShake.flashCanvas = Instantiate(prefab_flash, camara);
        }
        cameraShake.color = Color.white;

        cameraShake.Shake();
    }*/

    /*void SetCameraShake(Transform camara)
    {
        if (cameraShake == null)
        {
            cameraShake = camara.gameObject.GetComponent<CameraShake>();
        }
        if (cameraShake == null)
        {
            cameraShake = camara.gameObject.AddComponent<CameraShake>();
            GameObject pivot = new GameObject("pivot");
            pivot.transform.position = camara.position;
            pivot.transform.rotation = camara.rotation;
            camara.SetParent(pivot.transform);
        }
    }*/
}
