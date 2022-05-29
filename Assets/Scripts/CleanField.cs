using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanField : MonoBehaviour
{
    public float duration = 20;
    private List<Plate> plates;
    private float elapsedTime = 0;
    private bool running = false;
    public AudioSource runningSound;
    
    // Start is called before the first frame update
    void Start()
    {
        plates = new List<Plate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > duration) {
                CleanPlates();
                StopCleaning();
            }
        }
    }

    public void StartCleaning() {
        if (running) {
            return;
        }
        running = true;
        runningSound.Play();
        Debug.Log("play sounds");
    }

    public void StopCleaning() {
        if (!running) {
            return;
        }
        running = false;
        elapsedTime = 0;
        runningSound.Stop();
    }

    public void CleanPlates()
    {
        foreach (Plate plate in plates)
        {
            plate.Used = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Plate") {
            return;
        }
        var plate = other.gameObject.GetComponent<Plate>();
        if (!plate) {
            return;
        }
        if (!plates.Contains(plate))
        {
            plates.Add(other.gameObject.GetComponent<Plate>());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Plate") {
            return;
        }
        var plate = other.gameObject.GetComponent<Plate>();
        if (!plate) {
            return;
        }
        plates.Remove(other.gameObject.GetComponent<Plate>());
    }
}
