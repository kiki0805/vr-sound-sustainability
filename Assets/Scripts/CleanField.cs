using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanField : MonoBehaviour
{
    private List<Plate> plates;
    // Start is called before the first frame update
    void Start()
    {
        plates = new List<Plate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CleanPlates()
    {
        foreach (Plate plate in plates)
        {
            plate.Used = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Plate" && !plates.Contains(other.gameObject.GetComponent<Plate>()))
        {
            plates.Add(other.gameObject.GetComponent<Plate>());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Plate")
        {
            plates.Remove(other.gameObject.GetComponent<Plate>());
        }
    }
}
