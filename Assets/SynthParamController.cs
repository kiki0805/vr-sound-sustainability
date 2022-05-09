using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthParamController : MonoBehaviour
{
    AudioHelm.HelmController controller;
    public List<int> midiNotes;

    public bool OSC1Tune = false; // -100 to 100
    public bool ARPFreq = false; // 0.1s to 0.3s
    public bool AmpDecay = false; // 1s to 4s
    public bool OSC1Trans = false; // 0 to 15
    public bool auto = false;
    float elapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<AudioHelm.HelmController>();
    }

    void Update() {
        if (auto) {
            elapsedTime += Time.deltaTime;
            TuneParam(Mathf.Min(elapsedTime / 10f, 1));
        }
    }

    public void ResetTime() {
        elapsedTime = 0;
    }

    public void TuneParam(float pct) {
        if (OSC1Tune) {
            controller.SetParameterPercent(AudioHelm.Param.kOsc1Tune, pct);
        }
        if (ARPFreq) {
            controller.SetParameterPercent(AudioHelm.Param.kArpFrequency, 0.5f + pct * 0.3f);
        }
        if (AmpDecay) {
            controller.SetParameterPercent(AudioHelm.Param.kAmplitudeDecay, .2f + pct * 0.4f);
        }
        if (OSC1Trans) {
            controller.SetParameterPercent(AudioHelm.Param.kOsc1Transpose, 0.4f + pct * 0.2f);
        }
    }

    public void TuneFreq(float pct) {
        if (OSC1Trans) {
            controller.SetParameterPercent(AudioHelm.Param.kOsc1Transpose, 0.4f + pct * 0.2f);
        }
    }

    public void TuneDecay(float pct) {
        if (AmpDecay) {
            controller.SetParameterPercent(AudioHelm.Param.kAmplitudeDecay, .2f + pct * 0.4f);
        }
    }

    public void PlayNotes() {
        for (int i = 0; i < midiNotes.Count; i++) {
            controller.NoteOn(midiNotes[i], 1, 1);
        }
    }

    public void NotesOff() {
        for (int i = 0; i < midiNotes.Count; i++) {
            controller.NoteOff(midiNotes[i]);
        }
    }

    public void NotesOn() {
        for (int i = 0; i < midiNotes.Count; i++) {
            controller.NoteOn(midiNotes[i]);
        }
    }
}
