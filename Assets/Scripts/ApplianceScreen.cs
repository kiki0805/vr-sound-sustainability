using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceScreen : MonoBehaviour
{
    public GameObject EmissiveScreen;
    public bool ScreenPowerOn = false;
    public AudioSource OnSound;
    public AudioSource OffSound;
    Color onColor = new Color(24f / 255f, 191f / 255f, 0f / 255f);
    Color offColor = new Color(2f / 255f, 16f / 255f, 0f / 255f);
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = EmissiveScreen.GetComponent<Renderer>().material;
        if (ScreenPowerOn)
        {
            material.SetColor("_EmissionColor", onColor);
        }
        else
        {
            material.SetColor("_EmissionColor", offColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleScreen()
    {
        if (ScreenPowerOn) {
            ScreenOff();
            ScreenPowerOn = false;
        }
        else
        {
            ScreenOn();
            ScreenPowerOn = true;
        }
    }

    public void ScreenOn()
    {
        material.SetColor("_EmissionColor", onColor);
        if (OnSound != null) {
            OnSound.Play();
        }
    }

    public void ScreenOff()
    {
        material.SetColor("_EmissionColor", offColor);
        if (OffSound != null) {
            OffSound.Play();
        }
    }
}
