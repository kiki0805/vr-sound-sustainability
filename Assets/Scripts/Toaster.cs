using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Debug = Sisus.Debugging.Debug;

public class Toaster : MonoBehaviour
{
    public GameObject Handle;
    public Transform UpperPosition;
    public Transform LowerPosition;
    public HingeJoint hinge;
    public AudioSource UpSFX;
    public AudioSource DownSFX;
    public AudioSource TickSFX;
    public AudioSource DingSFX;
    public SynthParamController controller;

    List<IXRSelectInteractable> toasts = new List<IXRSelectInteractable>();
    float t = 0;
    bool movingHandle = false;
    bool moveUpwards = false;
    float remainedTime = 0;
    TaskTracker taskTracker;

    // Start is called before the first frame update
    void Start()
    {
        taskTracker = GameObject.Find("TaskTracker").GetComponent<TaskTracker>();
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
        // 10-30s mapping to 30-180 degrees
        if (hinge.angle < 20)
        {
            return;
        }
        hinge.useSpring = false;
        StartCooking();
        remainedTime = (hinge.angle / 180f) * 20 + 10; // 10-30s
        controller.TuneParam(hinge.angle / 180f);
        Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Set toaster time: {remainedTime}", Record.LogFileName);
    }

    void StartCooking()
    {
        DownSFX.Play();
        TickSFX.Play();
        controller.PlayNotes();
        movingHandle = true;
        moveUpwards = false;
    }

    void FinishCooking()
    {
        UpSFX.Play();
        TickSFX.Stop();
        DingSFX.Play();
        movingHandle = true;
        moveUpwards = true;
        hinge.useSpring = true;

        foreach (IXRSelectInteractable toast in toasts)
        {
            Transform toastObject = toast.transform;
            Food food = toastObject.gameObject.GetComponent<Food>();
            if (food.Cooked) continue;
            food.Cooked = true;
            taskTracker.toastDone = true;
            Debug.Log("toast done");
        }
    }
}
