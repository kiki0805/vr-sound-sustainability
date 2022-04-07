using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MyGrabInteractable : XRGrabInteractable
{
    public Transform RightAttach;
    public Transform LeftAttach;

    // void OnSelectEntering(SelectEnterEventArgs args)
    // {
    //     if (args.interactorObject.transform.gameObject.name == "RightHand Controller")
    //     {
    //         args.interactableObject.attachTransform = RightAttach;
    //         // args.interactorObject.attachTransform = RightAttach;
    //     }
    //     else
    //     {
    //         args.interactableObject.attachTransform = LeftAttach;
    //         // args.interactorObject.attachTransform = LeftAttach;
    //     }
    //     base.OnSelectEntering(args);
    // }
    public Transform GetAttachTransform(IXRInteractor interactor)
    {
        if (interactor.transform.gameObject.name == "RightHand Controller")
        {
            return RightAttach;
        }
        else
        {
            return LeftAttach;
        }
    }
}
