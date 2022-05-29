using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = Sisus.Debugging.Debug;

public class SynthParamController : MonoBehaviour
{
    AudioHelm.HelmController controller;
    AudioSource audioSource;
    public List<int> midiNotes;

    public bool OSC1Tune = false; // -100 to 100
    public bool ARPFreq = false; // 0.1s to 0.3s
    public bool AmpRelease = false; // 1s to 4s
    public bool OSC1Trans = false; // 0 to 15
    public bool auto = false;
    public int interval = 3; // default: 0.3
    public bool logOn = false;
    public bool reversePct = false;
    public float forceRelease = -1;
    bool mute = false;
    bool soundOn = false;
    float elapsedTime = 0;
    float realElapsedTime = 0;
    float lastTimeMute = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<AudioHelm.HelmController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        realElapsedTime += Time.deltaTime;
        if (auto) {
            elapsedTime += Time.deltaTime;
            TuneParam(Mathf.Min(elapsedTime / 10f, 1));
        }

        if (logOn) {
            Debug.LogChanges(() => realElapsedTime);
            Debug.Log((int)realElapsedTime % interval);
        }
        if (((int)realElapsedTime % interval == 0) && (realElapsedTime - lastTimeMute) > 2f) {
            mute = !mute;
            
            // audioSource.mute = !audioSource.mute;
            lastTimeMute = realElapsedTime;
            if (logOn) {
                Debug.Log("mute switch");
            }

            if (!soundOn) {
                return;
            }

            if (mute) {
                for (int i = 0; i < midiNotes.Count; i++) {
                    controller.NoteOff(midiNotes[i]);
                }
            }
            else {
                for (int i = 0; i < midiNotes.Count; i++) {
                    controller.NoteOn(midiNotes[i]);
                }
            }


        }
    }

    public void ResetTime() {
        elapsedTime = 0;
    }

    public void TuneParam(float pct) {
        if (reversePct) {
            pct = 1 - pct;
        }
        if (OSC1Tune) {
            controller.SetParameterPercent(AudioHelm.Param.kOsc1Tune, pct);
        }
        if (ARPFreq) {
            controller.SetParameterPercent(AudioHelm.Param.kArpFrequency, 0.5f + pct * 0.3f);
        }
        if (forceRelease != -1) {
            controller.SetParameterPercent(AudioHelm.Param.kAmplitudeRelease, forceRelease);
        }
        else if (AmpRelease) {
            controller.SetParameterPercent(AudioHelm.Param.kAmplitudeRelease, pct);
        }
        if (OSC1Trans) {
            controller.SetParameterPercent(AudioHelm.Param.kOsc1Transpose, 0.4f + pct * 0.2f);
        }
    }

    public void TuneFreq(float pct) {
        if (reversePct) {
            pct = 1 - pct;
        }
        if (OSC1Trans) {
            controller.SetParameterPercent(AudioHelm.Param.kOsc1Transpose, 0.4f + pct * 0.2f);
        }
    }

    public void TuneRelease(float pct) {
        if (reversePct) {
            pct = 1 - pct;
        }
        if (forceRelease != -1) {
            controller.SetParameterPercent(AudioHelm.Param.kAmplitudeRelease, forceRelease);
            return;
        }
        if (AmpRelease) {
            controller.SetParameterPercent(AudioHelm.Param.kAmplitudeRelease, pct);
        }
    }

    public void PlayNotes() {
        for (int i = 0; i < midiNotes.Count; i++) {
            controller.NoteOn(midiNotes[i], 1, 1);
        }
    }

    public void NotesOff() {
        soundOn = false;
        for (int i = 0; i < midiNotes.Count; i++) {
            controller.NoteOff(midiNotes[i]);
        }
    }

    public void NotesOn() {
        soundOn = true;
        if (mute) {
            return;
        }
        for (int i = 0; i < midiNotes.Count; i++) {
            controller.NoteOn(midiNotes[i]);
        }
    }
}
