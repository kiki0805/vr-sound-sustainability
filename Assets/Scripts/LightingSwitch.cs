using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Debug = Sisus.Debugging.Debug;

public class LightingSwitch : MonoBehaviour
{
    public GameObject Switch;
    public GameObject EmissiveObject;
    public bool lightEnabled = false;
    public AudioSource OnSound;
    public AudioSource OffSound;
    public UnityEvent LightOnEvent;
    public UnityEvent LightOffEvent;
    Vector3 eulerRotation;
    Color onColor = new Color(191f / 255f, 156f / 255f, 120f / 255f);
    Color offColor = new Color(16f / 255f, 13f / 255f, 10f / 255f);
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        if (Switch != null)
        {
            eulerRotation = Switch.transform.rotation.eulerAngles;
        }
        GetComponent<Light>().enabled = lightEnabled;
        if (EmissiveObject != null)
        {
            material = EmissiveObject.GetComponent<Renderer>().materials[1];
            if (lightEnabled)
            {
                material.SetColor("_EmissionColor", onColor);
            }
            else
            {
                material.SetColor("_EmissionColor", offColor);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ToggleEnabled() {
        bool lightEnabled = GetComponent<Light>().enabled;
        if (!lightEnabled) {
            if (Switch != null)
            {
                Switch.transform.rotation = Quaternion.Euler(8.5f, eulerRotation.y, eulerRotation.z);
            }
            if (EmissiveObject != null)
            {
                material.SetColor("_EmissionColor", onColor);
            }
            OnSound.Play();
            LightOnEvent.Invoke();
            Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Light on", Record.LogFileName);
        }
        else
        {
            if (Switch != null)
            {
                Switch.transform.rotation = Quaternion.Euler(-8.5f, eulerRotation.y, eulerRotation.z);
            }
            if (EmissiveObject != null)
            {
                material.SetColor("_EmissionColor", offColor);
            }
            OffSound.Play();
            LightOffEvent.Invoke();
            Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Light off", Record.LogFileName);
        }
        GetComponent<Light>().enabled = !lightEnabled;
    }
}
