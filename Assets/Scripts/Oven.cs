using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = Sisus.Debugging.Debug;

public class Oven : MonoBehaviour
{
    public HeatingField OvenHeatingField;
    public AudioSource OvenSound;
    public AudioSource FinishSound;
    public HingeJoint hinge;
    private float MinTime = 15;//15;
    private float MaxTime = 40;//40;
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
            Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Restore oven", Record.LogFileName);
        } else {
            pause = true;
            OvenSound.Pause();
            Debug.Log("Pause");
            Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Pause oven", Record.LogFileName);
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
                Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Start oven", Record.LogFileName);
            }
        }
        controller.PlayNotes();
        remainedTime = (MaxTime - MinTime) * pct + MinTime;
        Debug.Log("setTime");
        Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Set oven time {remainedTime}", Record.LogFileName);
    }
}
