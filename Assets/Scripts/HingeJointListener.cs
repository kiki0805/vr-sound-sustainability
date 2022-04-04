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
    float lastAngle;
    bool moving;

    HingeJoint hinge;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        lastAngle = hinge.angle;
    }

    // Update is called once per frame
    void Update()
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
        }
        if (hinge.velocity > 5 && !moving)
        {
            moving = true;
            StartMove.Invoke();
        }
        if (hinge.velocity < 5 && moving)
        {
            moving = false;
            StopMove.Invoke();
        }
    }
}
