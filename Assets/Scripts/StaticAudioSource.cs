using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAudioSource : MonoBehaviour
{
    AudioSource audioSource;
    float playTime;
    public float MinInterval = 60;
    public float MaxInterval = 60 * 10;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        updatePlayTime();
    }

    void updatePlayTime()
    {
        playTime = Time.time + Random.Range(MinInterval, MaxInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > playTime) {
            audioSource.Play();
            updatePlayTime();
        }
    }
}
