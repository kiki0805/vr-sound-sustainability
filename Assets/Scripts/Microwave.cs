using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : MonoBehaviour
{
    public HeatingField MicrowaveHeatingField;
    public AudioSource MicrowaveSound;
    public AudioSource FinishSound;
    private float MinTime = 15;
    private float MaxTime = 40;
    public HingeJoint hinge;
    float remainedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (remainedTime > 0 && MicrowaveHeatingField.Heating)
        {
            remainedTime -= Time.deltaTime;
            if (remainedTime <= 0)
            {
                MicrowaveHeatingField.Heating = false;
                MicrowaveSound.Stop();
            }
        }
    }

    public void SetTime()
    {
        float pct = (hinge.angle - hinge.limits.min) / (hinge.limits.max - hinge.limits.min);
        remainedTime = (MaxTime - MinTime) * pct + MinTime;
    }

    public void ToggleStart()
    {
        if (MicrowaveHeatingField.Heating)
        {
            MicrowaveHeatingField.Heating = false;
            MicrowaveSound.Stop();
            FinishSound.Play();
            hinge.useSpring = true;
        }
        else
        {
            MicrowaveHeatingField.Heating = true;
            MicrowaveSound.Play();
        }
    }
}
