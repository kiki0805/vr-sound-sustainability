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
    private ExperimentManager experimentManager;

    // Start is called before the first frame update
    void Start()
    {
        plates = (Plate[]) GameObject.FindObjectsOfType(typeof(Plate));
        experimentManager = GameObject.Find("ExperimentManager").GetComponent<ExperimentManager>();
        if (experimentManager.debug) {
            StartCoroutine(EnableTask1Done());
            friesDone = true;
            sandwichDone = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!task2Done.activeSelf) {
            if (friesDone && sandwichDone) {
                EnableTask2Done();
            }
        }

        if (task2Done.activeSelf && task1Done.activeSelf && !task3Done.activeSelf) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 3f) {
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
        if (task1Done.activeSelf) {
            return;
        }
        StartCoroutine(EnableTask1Done());
    }

    private IEnumerator EnableTask1Done() {
        yield return new WaitForSeconds(5);
        Debug.Log("Finish task1");
        task1Done.SetActive(true);
    }

    private void EnableTask2Done() {
        Debug.Log("Finish task2");
        task2Done.SetActive(true);
    }

    private void EnableTask3Done() {
        Debug.Log("Finish task3");
        task3Done.SetActive(true);
    }
}
