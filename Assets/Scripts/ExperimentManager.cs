using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ExperimentManager : MonoBehaviour
{
    public bool Pause = false;
    public GameObject RayInteractor;
    public GameObject MenuObject;
    public InputActionReference ControllerPressReference;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(ControllerPressReference.action.triggered)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (!Pause)
        {
            Pause = true;
            RayInteractor.SetActive(true);
            MenuObject.SetActive(true);
        }
        else
        {
            Pause = false;
            RayInteractor.SetActive(false);
            MenuObject.SetActive(false);
        }
    }
}
