using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CustomHand : MonoBehaviour
{
    //Stores handPrefab to be Instantiated
    public GameObject handPrefab;
    //Stores what kind of characteristics we're looking for with our Input Device when we search for it later
    public InputDeviceCharacteristics inputDeviceCharacteristics;

    //Stores the InputDevice that we're Targeting once we find it in InitializeHand()
    private InputDevice _targetDevice;
    private Animator _handAnimator;

    private float thumbValue;
    private readonly float thumbMoveSpeed = 0.1f;

    void Start()
    {
        InitializeHand();
    }

    private void InitializeHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //Call InputDevices to see if it can find any devices with the characteristics we're looking for
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        //Our hands might not be active and so they will not be generated from the search.
        //We check if any devices are found here to avoid errors.
        if (devices.Count > 0)
        {
            
            _targetDevice = devices[0];

            GameObject spawnedHand = Instantiate(handPrefab, transform);
            _handAnimator = spawnedHand.GetComponent<Animator>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        //Since our target device might not register at the start of the scene, we continously check until one is found.
        if(!_targetDevice.isValid)
        {
            InitializeHand();
        }
        else
        {
            UpdateHand();
        }
    }

    private void UpdateHand()
    {
        //This will get the value for our trigger from the target device and output a flaot into triggerValue
        if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            _handAnimator.SetFloat("Index", triggerValue);
        }
        else
        {
            _handAnimator.SetFloat("Index", 0);
        }
        //This will get the value for our grip from the target device and output a flaot into gripValue
        if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _handAnimator.SetFloat("Three Fingers", gripValue);
        }
        else
        {
            _handAnimator.SetFloat("Three Fingers", 0);
        }

        bool primaryTouchRetrieved = _targetDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out bool primaryTouched);
        bool secondaryTouchRetrieved = _targetDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out bool secondaryTouched);
        bool joystickTouchRetrieved = _targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool joystickTouched);
        if (primaryTouchRetrieved || secondaryTouchRetrieved || joystickTouchRetrieved)
        {
            if (primaryTouched || secondaryTouched || joystickTouched)
            {
                thumbValue += thumbMoveSpeed;
            }
            else
            {
                thumbValue -= thumbMoveSpeed;
            }

            thumbValue = Mathf.Clamp(thumbValue, 0, 1);
            _handAnimator.SetFloat("Thumb", thumbValue);
        }
    }


}