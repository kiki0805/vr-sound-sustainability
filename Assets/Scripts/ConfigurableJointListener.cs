using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PositionAxis
{
    X,
    Y,
    Z,
}

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
    public bool DebugOn = false;
    public PositionAxis Axis = PositionAxis.Z;
    public float repeatRate = 0.5f;
    private float timer = 0;

    ConfigurableJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        lastPosition = currentPosition;
        closePosition = (float)lastPosition;
        openPosition = (float)lastPosition - 0.4f;
        if (DebugOn)
        {
            Debug.Log($"closePosition {closePosition}");
            Debug.Log($"openPosition {openPosition}");
        }
    }

    float currentPosition
    {
        get
        {
            switch (Axis)
            {
                case PositionAxis.X:
                    return (float)transform.position.x;
                case PositionAxis.Y:
                    return (float)transform.position.y;
                case PositionAxis.Z:
                    return (float)transform.position.z;
                default:
                    return (float)transform.position.z;
            }
        }
    }

    float currentVelocity
    {
        get
        {
            switch (Axis)
            {
                case PositionAxis.X:
                    return GetComponent<Rigidbody>().velocity.x;
                case PositionAxis.Y:
                    return GetComponent<Rigidbody>().velocity.y;
                case PositionAxis.Z:
                    return GetComponent<Rigidbody>().velocity.z;
                default:
                    return GetComponent<Rigidbody>().velocity.z;
            }
        }
    }

    bool AlmostEqual(float v1, float v2)
    {
        return Mathf.Abs(v1 - v2) <= 0.001;
    }

    // Update is called once per frame
    void CheckState()
    {
        if (DebugOn)
        {
            // Debug.Log($"velocity {currentVelocity}");
            Debug.Log($"cur, last position {currentPosition} {lastPosition}");
            Debug.Log(Mathf.Round(currentPosition - openPosition));
            Debug.Log(Mathf.Round(currentPosition - closePosition));
        }

        if (AlmostEqual(currentPosition, closePosition) && !AlmostEqual(lastPosition, closePosition))
        {
            ReachMinLimit.Invoke();
            if (DebugOn)
            {
                Debug.Log("Reach min");
                
            }
        }
        if (AlmostEqual(currentPosition, openPosition) && !AlmostEqual(lastPosition, openPosition))
        {
            ReachMaxLimit.Invoke();
        }
        if (AlmostEqual(lastPosition, openPosition) && !AlmostEqual(lastPosition, currentPosition))
        {
            DetachMaxLimit.Invoke();
        }
        if (AlmostEqual(lastPosition, closePosition) && !AlmostEqual(lastPosition, currentPosition))
        {
            DetachMinLimit.Invoke();
            if (DebugOn)
            {
                Debug.Log("Detach min");
            }
        }
        if (!Mathf.Approximately(lastPosition, currentPosition))
        {
            lastPosition = currentPosition;
        }
        if (currentVelocity > 0 && !moving)
        {
            moving = true;
            StartMove.Invoke();
            if (DebugOn) Debug.Log("Start move");
        }
        if (currentVelocity == 0 && moving)
        {
            moving = false;
            StopMove.Invoke();
        }
    }

    void Update()
    {
        if (timer < 0)
        {
            CheckState();
            timer = repeatRate;
        }
        timer -= Time.deltaTime;
    }
}
