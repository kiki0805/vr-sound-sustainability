using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // collider = gameObject.GetComponent<Collder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEnabled() {
        GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
    }
}
