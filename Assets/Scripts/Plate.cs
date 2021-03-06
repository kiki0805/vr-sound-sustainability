using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = Sisus.Debugging.Debug;

public class Plate : MonoBehaviour
{
    public float Time2Wash = 5;
    public GameObject UsedVFX;
    GameObject usedVFXInstance;
    bool used = false;
    public bool alwaysClean = false;
    public bool Used {
        get {
            return used;
        }
        set {
            if (alwaysClean) {
                return;
            }
            if (!used && value)
            {
                used = value;
                usedVFXInstance = Instantiate(UsedVFX, gameObject.transform, false);
                Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Use a clean plate", Record.LogFileName);
            }
            if (used && !value)
            {
                used = value;
                Destroy(usedVFXInstance);
                Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Clean a plate", Record.LogFileName);
            }
        }
    }

    float usingTime = 0;
    bool underUsing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (underUsing && !Used)
        {
            usingTime += Time.deltaTime;
            if (usingTime > Time2Wash)
            {
                Used = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Food" && !underUsing)
        {
            underUsing = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Food" && underUsing)
        {
            underUsing = false;
        }
    }
}
