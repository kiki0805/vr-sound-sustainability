using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ForceEnableSpatialize : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.spatialize) {
            audioSource.spatialize = true;
        }
    }
}
