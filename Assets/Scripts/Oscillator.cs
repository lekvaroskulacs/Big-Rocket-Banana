using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    private float movementFactor;
    [SerializeField] private float speed;


    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (speed == 0) return;

        movementFactor = Mathf.Sin(Time.time * speed) + 1;
        movementFactor /= 2;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
