using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfigurableJointListener : MonoBehaviour
{
    public UnityEvent ReachMinLimit;
    public UnityEvent ReachMaxLimit;
    public UnityEvent DetachMinLimit;
    public UnityEvent DetachMaxLimit;
    public UnityEvent StartMove;
    public UnityEvent StopMove;
    float closePosition;
    float openPosition;
    float lastPosition;
    bool moving;

    ConfigurableJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        lastPosition = (float)transform.position.z;
        closePosition = (float)lastPosition;
        openPosition = (float)lastPosition + 0.4f;
        Debug.Log($"closePosition {closePosition}");
        Debug.Log($"openPosition {openPosition}");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"velocity {GetComponent<Rigidbody>().velocity.z}");
        Debug.Log($"position {transform.position.z}");

        if(Mathf.Round(transform.position.z - closePosition) == 0 && Mathf.Round(lastPosition - closePosition) != 0)
        {
            ReachMinLimit.Invoke();
        }
        if(Mathf.Round(transform.position.z - openPosition) == 0 && Mathf.Round(lastPosition - openPosition) != 0)
        {
            ReachMaxLimit.Invoke();
        }
        if (Mathf.Round(lastPosition - openPosition) == 0 && Mathf.Round(transform.position.z - lastPosition) != 0)
        {
            DetachMaxLimit.Invoke();
        }
        if (Mathf.Round(closePosition) == 0 && Mathf.Round(transform.position.z - lastPosition) != 0)
        {
            DetachMinLimit.Invoke();
        }
        if (Mathf.Round(lastPosition - transform.position.z) != 0)
        {
            lastPosition = (float)transform.position.z;
        }
        if (GetComponent<Rigidbody>().velocity.z > 5 && !moving)
        {
            moving = true;
            StartMove.Invoke();
        }
        if (GetComponent<Rigidbody>().velocity.z < 5 && moving)
        {
            moving = false;
            StopMove.Invoke();
        }
    }
}