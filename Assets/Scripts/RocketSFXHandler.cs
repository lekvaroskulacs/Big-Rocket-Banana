using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSFXHandler : MonoBehaviour
{
    private AudioSource thrusting;
    private Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        thrusting = GetComponent<AudioSource>();
        movement.thrustEnable += thrustEnableSFX;
        movement.thrustDisable += thrustDisableSFX;
    }

    private void thrustEnableSFX()
    {
        thrusting.time = 0.5f;
        thrusting?.Play();
    }

    private void thrustDisableSFX()
    {
        thrusting?.Stop();
    }
}
