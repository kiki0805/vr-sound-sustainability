using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MyGrabInteractable : XRGrabInteractable
{
    public Transform RightAttach;
    public Transform LeftAttach;

    void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.name == "RightHand Controller")
        {
            attachTransform = RightAttach;
        }
        else
        {
            attachTransform = LeftAttach;
        }
        base.OnSelectEntering(args);
    }
}
