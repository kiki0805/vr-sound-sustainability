using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TaskTracker : MonoBehaviour
{
    public GameObject task1Done;
    public GameObject task2Done;
    public GameObject task3Done;
    private Plate[] plates;
    public bool friesDone = false;
    public bool sandwichDone = false;
    public VideoPlayer playerObject;
    private float elapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        plates = (Plate[]) GameObject.FindObjectsOfType(typeof(Plate));
    }

    // Update is called once per frame
    void Update()
    {
        if (!task2Done.active) {
            if (friesDone && sandwichDone) {
                EnableTask2Done();
            }
        }

        if (task2Done.active && task1Done.active && !task3Done.active) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 10f) {
                CheckTask3();
                elapsedTime = 0;
            }
        }
    }

    private void CheckTask3() {
        Plate plate;
        bool allClean = true;
        for (int i = 0; i < plates.Length; i++) {
            plate = plates[i];
            if (plate.Used) {
                allClean = false;
                break;
            }
        }
        if (!allClean) {
            return;
        }
        if (playerObject.isPlaying) {
            EnableTask3Done();
        }
    }

    public void FinishPlayingMusic() {
        if (task1Done.active) {
            return;
        }
        StartCoroutine(EnableTask1Done());
    }

    private IEnumerator EnableTask1Done() {
        yield return new WaitForSeconds(5);
        task1Done.SetActive(true);
    }

    private void EnableTask2Done() {
        task2Done.SetActive(true);
    }

    private void EnableTask3Done() {
        task3Done.SetActive(true);
    }
}
