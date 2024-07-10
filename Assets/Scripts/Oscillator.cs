using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;                                   // get position of wherever obj first starts. 
    [SerializeField] Vector3 movementVector;                    // determines SPEED of movement
    [SerializeField] [Range(0, 1)] float movementFactor;                                       // creates a FUCKING SLIDER from 0-1
    [SerializeField] float period = 2f;           // initialize a period of 2

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // continually growing over time:
        float cycles = Time.time / period;      // if time is 10 secs and period is 2, we'll have 5 cycles by then.

        const float tau = Mathf.PI * 2; 
        float rawSinWave = Mathf.Sin(cycles * tau);         // accepts input angle, in RADIANS. Returns value between 1 and -1.

        movementFactor = (rawSinWave + 1f) / 2f;                  // we want to go from 0-1 instead of -1 - 1. This is turn is /2 so we finally get value that is 0-1.

        Vector3 offset = movementVector * movementFactor;     // if 1st is (10, 0, 0) and 2nd is 0.5, offset is (5, 0, 0)
        // Vector3 newPosition = startingPosition + offset;                                            // define new position of obj that's offset from original position
        
        // transform.position += Vector3.right;
        // why doesn't transform.position += offset; work the same? Because that is a DYNAMIC value, it'll keep moving right. The other one, though, is static. 
        transform.position = startingPosition + offset;
    }
}
