using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = Sisus.Debugging.Debug;

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
        Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Set microwave time: {remainedTime}", Record.LogFileName);
    }

    public void ToggleStart()
    {
        if (MicrowaveHeatingField.Heating)
        {
            MicrowaveHeatingField.Heating = false;
            MicrowaveSound.Stop();
            FinishSound.Play();
            hinge.useSpring = true;
            Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] pause microwave", Record.LogFileName);
        }
        else
        {
            MicrowaveHeatingField.Heating = true;
            MicrowaveSound.Play();
            Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] restore microwave", Record.LogFileName);
        }
    }
}
