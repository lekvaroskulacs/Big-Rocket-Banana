using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSFXHandler : MonoBehaviour
{

    [SerializeField] AudioClip engine;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip success;

    private AudioSource source;


    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void thrustEnableSFX()
    {
        source.time = 0.5f;
        source.loop = true;
        source?.PlayOneShot(engine);
    }

    public void thrustDisableSFX()
    {
        source?.Stop();
        source.loop = false;
    }

    public void explosionSFX()
    {
        
        if (!source.isPlaying)
            source.PlayOneShot(explosion);
    }

    public void successSFX()
    {
        if (!source.isPlaying)
            source.PlayOneShot(success);
    }
}
