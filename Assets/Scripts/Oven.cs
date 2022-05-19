using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public HeatingField OvenHeatingField;
    public AudioSource OvenSound;
    public AudioSource FinishSound;
    public HingeJoint hinge;
    public float MinTime = 5;//15;
    public float MaxTime = 7;//40;
    private bool pause = false;
    float remainedTime = 0;
    public SynthParamController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (remainedTime > 0 && !pause)
        {
            remainedTime -= Time.deltaTime;
            if (remainedTime <= 0)
            {
                OvenHeatingField.Heating = false;
                OvenSound.Stop();
                FinishSound.Play();
                hinge.useSpring = true;
                remainedTime = 0;
            }
        }
    }

    public void Pause() {
        if (remainedTime == 0) {
            return;
        }
        if (pause) {
            pause = false;
            OvenSound.Play();
            Debug.Log("Recover");
        } else {
            pause = true;
            OvenSound.Pause();
            Debug.Log("Pause");
        }
    }

    public void SetTime()
    {
        float pct = (hinge.angle - hinge.limits.min) / (hinge.limits.max - hinge.limits.min);
        if (pct < 0.1 && !controller.reversePct) return;
        if (pct > 0.9 && controller.reversePct) return;
        controller.TuneRelease(pct);
        if (remainedTime == 0) {
            OvenHeatingField.Heating = true;
            if (!pause) {
                OvenSound.Play();
            }
        }
        controller.PlayNotes();
        remainedTime = (MaxTime - MinTime) * pct + MinTime;
        Debug.Log("setTime");
    }
}
