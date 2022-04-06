using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    AudioSource audioSource;
    bool powerOn = false;
    // Start is called before the first frameupdate
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseVolume()
    {
        var curVol = audioSource.volume;
        curVol += 0.2f;
        curVol = Mathf.Min(curVol, 1f);
        audioSource.volume = curVol;
    }

    public void DecreaseVolume()
    {
        var curVol = audioSource.volume;
        curVol -= 0.2f;
        curVol = Mathf.Max(curVol, 0f);
        audioSource.volume = curVol;
    }

    public void TogglePower()
    {
        if (powerOn)
        {
            powerOn = false;
            audioSource.Stop();
        }
        else
        {
            powerOn = true;
            audioSource.Play();
        }
    }
}
