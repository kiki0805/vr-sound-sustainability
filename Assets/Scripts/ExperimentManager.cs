using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public enum Group {
    Group1,
    Group2,
    Group3,
}

public class ExperimentManager : MonoBehaviour
{
    public bool Pause = false;
    public GameObject RayInteractor;
    public GameObject MenuObject;
    public InputActionReference ControllerPressReference;
    public Group currentGroup;
    public AudioMixerSnapshot group1Snapshot;
    public AudioMixerSnapshot group2Snapshot;
    public AudioMixerSnapshot group3Snapshot;

    void Start()
    {
        
    }

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
