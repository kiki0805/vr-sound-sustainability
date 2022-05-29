using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsageMonitor : MonoBehaviour
{
    public List<AudioDistortionFilter> filters;
    private List<AudioChorusFilter> chorusFilters;
    public int usingRef = 0;
    public float monitorDuration = 10;
    public float updateFreq = 0.5f; // seconds
    public float maxUsingTime = 15; // seconds
    public int maxUsingCount = 4;
    public float maxDistortionLevel = 0.7f;
    public ExperimentManager manager;
    private float elapsedTime = 0;
    private int usingCount = 0;
    private int UsingCount {
        get {
            return usingCount;
        }
        set {
            usingCount = value;
            UpdateFilters((float)Mathf.Min(maxUsingCount, usingCount) / (float)maxUsingCount);
        }
    }
    private float usingTime = 0;

    void Awake() {
        chorusFilters = new List<AudioChorusFilter>();
        AudioDistortionFilter filter;
        for (int i = 0; i < filters.Count; i++) {
            filter = filters[i];
            var chorusFilter = filter.gameObject.GetComponent<AudioChorusFilter>();
            if (chorusFilter) {
                chorusFilters.Add(chorusFilter);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("ExperimentManager").GetComponent<ExperimentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (usingRef != 0) {
            usingTime += Time.deltaTime;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > updateFreq) {
                UpdateFilters(Mathf.Min(maxUsingTime, usingTime) / maxUsingTime);
                elapsedTime = 0;
            }
        }
        else if (usingTime != 0) {
            usingTime = 0;
            elapsedTime = 0;
            UpdateFilters(0);
        }
    }

    public void AddUsingRefer() {
        usingRef ++;
    }

    public void RemoveUsingRefer() {
        usingRef --;
    }

    public void UseOnce() {
        UsingCount ++;
        StartCoroutine(WaitAndDecreaseUsingTime());
    }

    private IEnumerator WaitAndDecreaseUsingTime() {
        yield return new WaitForSeconds(monitorDuration);
        UsingCount --;
        if (UsingCount < 0) {
            UsingCount = 0;
        }
    }

    private void UpdateFilters(float level) {
        if (manager.currentGroup != Group.Group2) {
            return;
        }
        for (int i = 0; i < filters.Count; i++) {
            var filter = filters[i];
            // filter.distortionLevel = level * maxDistortionLevel;
        }
        for (int i = 0; i < chorusFilters.Count; i++) {
            var chorusFilter = chorusFilters[i];
            chorusFilter.depth = 0.3f * level + 0.5f;
            chorusFilter.dryMix = (1 - level) * 1;
            chorusFilter.wetMix1 = level;
            chorusFilter.wetMix2 = level;
            chorusFilter.wetMix3 = level;
            chorusFilter.delay = (1 - level) * 30 + 10;
            chorusFilter.rate = 0.8f + level * 10;
        }
    }
}
