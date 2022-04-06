using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasFire : MonoBehaviour
{
    public float MinSpeed = 0;
    public float MaxSpeed = 15;
    public AudioSource FireSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeed(float pct)
    {
        float speed = MaxSpeed * pct;
        var emission = GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = speed;
        FireSound.volume = pct;
    }
}
