using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;
using VRUiKits.Utils;

public enum Group {
    Group1,
    Group2,
    Group3,
}

public class ExperimentManager : MonoBehaviour
{
    public bool debug = false;
    public bool Pause = false;
    public GameObject RayInteractor;
    public GameObject MenuObject;
    public InputActionReference ControllerPressReference;
    public OptionsManager optionsManager;
    public Group currentGroup;
    public AudioMixerSnapshot group1Snapshot;
    // public AudioMixerSnapshot group2Snapshot;
    public AudioMixerSnapshot group3Snapshot;

    void Start()
    {
        optionsManager.OnOptionSelected += SwitchSnapshot;
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

    public void SwitchSnapshot(int index) {
        switch (index) {
            case 0:
                currentGroup = Group.Group1;
                group1Snapshot.TransitionTo(1);
                Debug.Log("Transit to group1");
                break;
            case 1:
                currentGroup = Group.Group2;
                group1Snapshot.TransitionTo(1);
                Debug.Log("Transit to group2");
                break;
            case 2:
                currentGroup = Group.Group3;
                group3Snapshot.TransitionTo(1);
                Debug.Log("Transit to group3");
                break;
            default:
                return;
        }
    }
}
