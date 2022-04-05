using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasFire : MonoBehaviour
{
    public float MinSpeed = 0;
    public float MaxSpeed = 15;
    ParticleSystem particleSystem;
    public AudioSource FireSound;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeed(float pct)
    {
        float speed = MaxSpeed * pct;
        var emission = particleSystem.emission;
        emission.rateOverTime = speed;
        FireSound.volume = pct;
    }
}
