using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = Sisus.Debugging.Debug;

public class TestHelm : MonoBehaviour
{
    AudioHelm.HelmController controller;
    public int midiNote = 72;
    public bool trigger = false;
    public bool stopTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<AudioHelm.HelmController>();
        // controller.NoteOn(midiNote, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger) {
            controller.NoteOff(midiNote);
            controller.NoteOn(midiNote);
            trigger = false;
        }
        if (stopTrigger) {
            controller.NoteOff(midiNote);
            stopTrigger = false;
        }
    }
}
