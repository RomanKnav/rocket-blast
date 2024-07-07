using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;      // the namespace that MonoBehavior lives in

public class MovementScript : MonoBehaviour
{
    [SerializeField] float thrust = 1000f;
    [SerializeField] float rotate = 500f;

    private Rigidbody rb;    // MEMBER var: available everywhere in the class.
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // THERE'S A BUG IN HERE (I think: the log statements only log once?)
    // actual bug: we don't wanna rotate left/right at the same time.
    void ProcessThrust()
    {
        // remember: true WHILE key is pressed down:
        if (Input.GetKey(KeyCode.Space)) {
            Debug.Log("Pressed Space -Thrusting");

            // applies force RELATIVE to obj's position. Example, if facing right and pressed thrust, will move right instead of up.
            // rb.AddRelativeForce(0, 1, 0);   // takes a vector3 as arg. Can also be written as:

            rb.AddRelativeForce(thrust * Time.deltaTime * UnityEngine.Vector3.up);
            // rb.AddRelativeForce(UnityEngine.Vector3.up);

            // only plays AFTER key is released:
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
           
        } else {
            audioSource.Stop();
        }
    }

    void ProcessRotation() {

        // only the z-axis tilt should change (for ship to tilt forward/back):
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Pressed A -Rotate Left");
            ApplyRotation(rotate);
        }

        else if (Input.GetKey(KeyCode.D)) {     // ensures D is pressed only if A is not pressed
            Debug.Log("Pressed D -Rotate Right");
            ApplyRotation(-rotate);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        // freezing rotation so we can manually rotate (???)
        rb.freezeRotation = true;   // how does this not freeze the entire rotation?
        transform.Rotate(rotationThisFrame * Time.deltaTime * UnityEngine.Vector3.forward);
        // rb.freezeRotation = false;  // unfreezing rotation so that physics system can take over.
    }
}