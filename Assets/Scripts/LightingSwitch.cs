using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSwitch : MonoBehaviour
{
    public GameObject Switch;
    public GameObject EmissiveObject;
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
        if (EmissiveObject != null)
        {
            material = EmissiveObject.GetComponent<Renderer>().materials[1];
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
                Debug.Log(material.GetColor("_EmissionColor"));
                material.SetColor("_EmissionColor", onColor);
            }
        }
        else
        {
            if (Switch != null)
            {
                Switch.transform.rotation = Quaternion.Euler(-8.5f, eulerRotation.y, eulerRotation.z);
            }
            if (EmissiveObject != null)
            {
                Debug.Log(material.GetColor("_EmissionColor"));
                material.SetColor("_EmissionColor", offColor);
            }
        }
        GetComponent<Light>().enabled = !lightEnabled;
    }
}
