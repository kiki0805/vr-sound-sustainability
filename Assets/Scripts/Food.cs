using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = Sisus.Debugging.Debug;

public class Food : MonoBehaviour
{
    public float RequiredCookingTime = 10;
    public GameObject CookedVFX;
    HeatingField heatingField;
    bool cooked = false;
    public bool Cooked {
        set {
            if (!cooked && value)
            {
                cooked = value;
                setCooked();
            }
        }
        get {
            return cooked;
        }
    }
    bool cooking = false;
    float currentCookedTime = 0;
    TaskTracker taskTracker;

    // Start is called before the first frame update
    void Start()
    {
        taskTracker = GameObject.Find("TaskTracker").GetComponent<TaskTracker>();
    }

    void setCooked()
    {
        Instantiate(CookedVFX, gameObject.transform, false);
        var ingredient = GetComponent<Ingredient>();
        if (ingredient) {
            if (!heatingField) {
                ingredient.cookedBy = CookingMethod.ByToaster;
                return;
            }
            ingredient.cookedBy = heatingField.cookingMethod;
        }
        if (gameObject.name == "Chicken") {
            taskTracker.chickenDone = true;
            Debug.Log("chicken done");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cooking && !Cooked)
        {
            currentCookedTime += (Time.deltaTime * heatingField.HeatingSpeed);
            if (currentCookedTime >= RequiredCookingTime)
            {
                Cooked = true;
            }
        }
    }

    // void OnCollisionEnter(Collision other) {
    //     Debug.Log("collision enter in food");
    //     // if (other.gameObject.tag == "Heating Field" && !cooking)
    //     // {
    //     //     Debug.Log("start cooking");
    //     //     cooking = true;
    //     //     heatingField = other.gameObject.GetComponent<HeatingField>();
    //     // }
    // }

    // void OnCollisionExit(Collision other) {
    //     Debug.Log("collision exit in food");
    //     // if (other.gameObject.tag == "Heating Field" && cooking)
    //     // {
    //     //     Debug.Log("stop cooking");
    //     //     cooking = false;
    //     // }
    // }

    void OnTriggerEnter(Collider other) {
        // Debug.Log("trigger enter in food");
        // Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "HeatingField" && !cooking)
        {
            Debug.Log("start cooking");
            cooking = true;
            heatingField = other.gameObject.GetComponent<HeatingField>();
            if (!heatingField) {
                Debug.LogState(this);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        // Debug.Log("trigger exit in food");
        // Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "HeatingField" && cooking)
        {
            HeatingField exitHeatingField = other.gameObject.GetComponent<HeatingField>();
            if (GameObject.ReferenceEquals(heatingField.gameObject, exitHeatingField.gameObject)) {
                Debug.Log("stop cooking");
                cooking = false;
            }
        }
    }
}
