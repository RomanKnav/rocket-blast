using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;                                   // get position of wherever obj first starts. 
    [SerializeField] Vector3 movementVector;                     // determines SPEED of movement
    [SerializeField] [Range(0, 1)] float movementFactor;        // creates a FUCKING SLIDER from 0-1

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementVector * movementFactor;     // if 1st is (10, 0, 0) and 2nd is 0.5, offset is (5, 0, 0)
        // Vector3 newPosition = startingPosition + offset;                                            // define new position of obj that's offset from original position
        
        // transform.position += Vector3.right;
        // why doesn't transform.position += offset; work the same?
        transform.position = startingPosition +  offset;
    }
}
