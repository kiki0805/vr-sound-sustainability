using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsageMonitor : MonoBehaviour
{
    public List<AudioDistortionFilter> filters;
    public int usingRef = 0;
    public float monitorDuration = 10;
    public float updateFreq = 0.5f; // seconds
    public float maxUsingTime = 15; // seconds
    public int maxUsingCount = 4;
    public float maxDistortionLevel = 0.7f;
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

    // Start is called before the first frame update
    void Start()
    {
        
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
        for (int i = 0; i < filters.Count; i++) {
            var filter = filters[i];
            filter.distortionLevel = level * maxDistortionLevel;
        }
    }
}
