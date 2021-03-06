using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HingeJointListener: MonoBehaviour
{
    public UnityEvent ReachMinLimit;
    public UnityEvent ReachMaxLimit;
    public UnityEvent DetachMinLimit;
    public UnityEvent DetachMaxLimit;
    public UnityEvent StartMove;
    public UnityEvent StopMove;
    public GasFire GasFireController;
    public HeatingField HeatingPowerController;
    public SynthParamController controller;
    float lastAngle;
    bool moving = false;
    private float repeatRate = 0.05f;
    private float timer = 0;

    HingeJoint hinge;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        lastAngle = hinge.angle;
    }

    void Update() {
        if (timer < 0)
        {
            CheckState();
            timer = repeatRate;
        }
        timer -= Time.deltaTime;
    }

    // Update is called once per frame
    void CheckState()
    {
        // Debug.Log(hinge.velocity);
        if(Mathf.Round(hinge.angle - hinge.limits.min) == 0 && Mathf.Round(lastAngle - hinge.limits.min) != 0)
        {
            ReachMinLimit.Invoke();
        }
        if(Mathf.Round(hinge.angle - hinge.limits.max) == 0 && Mathf.Round(lastAngle - hinge.limits.max) != 0)
        {
            ReachMaxLimit.Invoke();
        }
        if (Mathf.Round(lastAngle - hinge.limits.max) == 0 && Mathf.Round(hinge.angle - lastAngle) != 0)
        {
            DetachMaxLimit.Invoke();
        }
        if (Mathf.Round(lastAngle - hinge.limits.min) == 0 && Mathf.Round(hinge.angle - lastAngle) != 0)
        {
            DetachMinLimit.Invoke();
        }
        if (Mathf.Round(hinge.angle - lastAngle) != 0)
        {
            lastAngle = hinge.angle;
            var speed = (hinge.angle - hinge.limits.min) / (hinge.limits.max - hinge.limits.min);
            if (GasFireController != null)
            {
                GasFireController.SetSpeed(speed);
                if (controller != null) {
                    controller.TuneParam(speed);
                }
            }
            if (HeatingPowerController != null)
            {
                HeatingPowerController.HeatingSpeed = speed;
            }
            if (controller != null) {
                controller.TuneFreq(speed);
            }
        }
        if (hinge.velocity > 0 && !moving)
        {
            moving = true;
            // buggy
            StartMove.Invoke();
            // Debug.Log("start move");
        }
        if (hinge.velocity == 0 && moving)
        {
            moving = false;
            // buggy
            StopMove.Invoke();
            // Debug.Log("stop move");
        }
    }
}
