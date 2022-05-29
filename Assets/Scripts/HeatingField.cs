using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingField : MonoBehaviour
{
    public bool Heating = true;
    public CookingMethod cookingMethod;
    public float heatingSpeed = 0; // 0-1
    public float HeatingSpeed {
        get {
            if (Heating)
            {
                return heatingSpeed;
            }
            return 0;
        }

        set {
            heatingSpeed = value;
        }
    }
}
