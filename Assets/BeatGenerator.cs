using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = Sisus.Debugging.Debug;

[RequireComponent(typeof(AudioHelm.Sequencer))]
public class BeatGenerator : MonoBehaviour
{
    public List<LightingSwitch> lights;
    public int midiNote = 60; // C3
    [ShowOnly] public int lightNum = 10; // default for test
    [ShowOnly] public int onLightNum = -1;
    public int OnLightNum {
        get {
            return onLightNum;
        }
        set {
            sequencer.Clear();
            onLightNum = Mathf.Clamp(value, 0, lightNum);

            int increasement = (int)Mathf.Pow(2, Mathf.RoundToInt(3 * ((float)(lightNum - onLightNum) / (float)lightNum) + 1));
            for (int i = 0; i < sequencer.length; i += increasement) {
                sequencer.AddNote(midiNote, i, i+2);
            }
        }
    }
    public bool testIncrease = false;
    public bool testDecrease = false;
    private AudioHelm.Sequencer sequencer;

    void Start() {
        sequencer = GetComponent<AudioHelm.Sequencer>();
        if (lights.Count == 0) {
            OnLightNum = 0;
            return;
        }
        lightNum = lights.Count;
        for (int i = 0; i < lightNum; i++) {
            lights[i].LightOnEvent.AddListener(IncreaseOnLightNum);
            lights[i].LightOffEvent.AddListener(DecreaseOnLightNum);
            if (lights[i].lightEnabled) {
                onLightNum ++;
            }
        }
        OnLightNum = onLightNum;
    }

    void Update() {
        if (testIncrease) {
            IncreaseOnLightNum();
            testIncrease = false;
        }
        else if (testDecrease) {
            DecreaseOnLightNum();
            testDecrease = false;
        }
    }

    public void IncreaseOnLightNum() {
        OnLightNum ++;
    }

    public void DecreaseOnLightNum() {
        OnLightNum --;
    }
}
