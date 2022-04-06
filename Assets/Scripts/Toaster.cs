using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Toaster : MonoBehaviour
{
    public GameObject Handle;
    public Transform UpperPosition;
    public Transform LowerPosition;
    public HingeJoint hinge;
    public GameObject CookedVFX;
    public AudioSource UpSFX;
    public AudioSource DownSFX;
    public AudioSource TickSFX;
    public AudioSource DingSFX;

    List<IXRSelectInteractable> toasts = new List<IXRSelectInteractable>();
    float t = 0;
    bool movingHandle = false;
    bool moveUpwards = false;
    float remainedTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddToast(SelectEnterEventArgs args)
    {
        IXRSelectInteractable toast = args.interactableObject;
        toasts.Add(toast);
    }

    public void RemoveToast(SelectExitEventArgs args)
    {
        IXRSelectInteractable toast = args.interactableObject;
        toasts.Remove(toast);
    }

    // Update is called once per frame
    void Update()
    {
        if (remainedTime > 0)
        {
            remainedTime -= Time.deltaTime;
            if (remainedTime <= 0)
            {
                FinishCooking();
            }
        }

        if (!movingHandle) return;
        t += Time.deltaTime;
        if (moveUpwards)
        {
            Handle.transform.position = Vector3.Lerp(LowerPosition.position, UpperPosition.position, t);
        }
        else
        {
            Handle.transform.position = Vector3.Lerp(UpperPosition.position, LowerPosition.position, t);
        }
        if (t >= 1)
        {
            t = 0;
            movingHandle = false;
            if (moveUpwards)
            {
                hinge.useSpring = false;
            }
        }
    }

    public void SetTime()
    {
        Debug.Log("Try to set time");
        // 10-30s mapping to 30-180 degrees
        if (hinge.angle < 20)
        {
            return;
        }
        hinge.useSpring = false;
        StartCooking();
        remainedTime = (hinge.angle / 180f) * 5 + 5; // 5-10s
        Debug.Log($"Remained {remainedTime}");
    }

    void StartCooking()
    {
        Debug.Log("Start cooking");
        DownSFX.Play();
        TickSFX.Play();
        movingHandle = true;
        moveUpwards = false;
    }

    void FinishCooking()
    {
        Debug.Log("Finish cooking");
        UpSFX.Play();
        TickSFX.Stop();
        DingSFX.Play();
        movingHandle = true;
        moveUpwards = true;
        hinge.useSpring = true;

        foreach (IXRSelectInteractable toast in toasts)
        {
            Transform toastObject = toast.transform;
            if (toastObject.gameObject.tag == "Cooked") continue;
            GameObject cookedVFX = Instantiate(CookedVFX, toastObject, false) as GameObject;
            toastObject.gameObject.tag = "Cooked";
        }
    }
}
