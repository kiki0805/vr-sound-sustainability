using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Debug = Sisus.Debugging.Debug;

public class TaskTracker : MonoBehaviour
{
    public GameObject task1Done;
    public GameObject task2Done;
    public GameObject task3Done;
    private Plate[] plates;
    public bool chickenDone = false;
    public bool friedEggDone = false;
    public bool toastDone = false;
    public VideoPlayer playerObject;
    private float elapsedTime = 0;
    private ExperimentManager experimentManager;

    // Start is called before the first frame update
    void Start()
    {
        plates = (Plate[]) GameObject.FindObjectsOfType(typeof(Plate));
        experimentManager = GameObject.Find("ExperimentManager").GetComponent<ExperimentManager>();
        if (experimentManager.debug) {
            EnableTask1Done();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!task2Done.activeSelf) {
            if (chickenDone && friedEggDone && toastDone) {
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
        EnableTask3Done();
    }

    public void EnterLivingRoom() {
        EnableTask1Done();
    }

    private void EnableTask1Done() {
        Debug.Log("Finish task1");
        Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Finish task1", Record.LogFileName);
        task1Done.SetActive(true);
    }

    private void EnableTask2Done() {
        Debug.Log("Finish task2");
        Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Finish task2", Record.LogFileName);
        task2Done.SetActive(true);
    }

    private void EnableTask3Done() {
        Debug.Log("Finish task3");
        Debug.LogToFile($"[{System.DateTime.Now.ToString("MM/dd HH:mm:ss.fff")}] Finish task3", Record.LogFileName);
        task3Done.SetActive(true);
    }
}
