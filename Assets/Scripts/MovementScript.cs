using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;      // the namespace that MonoBehavior lives in

public class MovementScript : MonoBehaviour
{
    [SerializeField] float thrust = 1000f;
    [SerializeField] float rotate = 500f;
    [SerializeField] AudioClip rocketEngine;    
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;

    private Rigidbody rb;    // MEMBER var: available everywhere in the class.
    private AudioSource audioSource;        // AudioClip is what is serialized, and AudioSource is what's used in the code. 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();  // this is a component that's left manually empty.
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // THRUSTING CRAP:
    void ProcessThrust()
    {
        // remember: true WHILE key is pressed down:
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrust()
    {
        // applies force RELATIVE to obj's position. Example, if facing right and pressed thrust, will move right instead of up.
        // rb.AddRelativeForce(0, 1, 0);   // takes a vector3 as arg. Can also be written as:

        rb.AddRelativeForce(thrust * Time.deltaTime * UnityEngine.Vector3.up);
        // rb.AddRelativeForce(UnityEngine.Vector3.up);

        // only plays AFTER key is released:
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rocketEngine);
        }
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        thrustParticles.Stop();
    }

    // ROTATING CRAP:
    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A))
        {
            ProcessLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {     // ensures D is pressed only if A is not pressed
            ProcessRight();
        }

        else
        {
            StopParticles();
        }
    }

    void ProcessRight()
    {
        Debug.Log("Pressed D -Rotate Right");
        if (!rightParticles.isPlaying)
        {
            rightParticles.Play();
        }
        ApplyRotation(-rotate);
    }

    void ProcessLeft()
    {
        Debug.Log("Pressed A -Rotate Left");
        if (!leftParticles.isPlaying)
        {
            leftParticles.Play();
        }
        ApplyRotation(rotate);
    }

    private void StopParticles()
    {
        leftParticles.Stop();
        rightParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        // freezing rotation so we can manually rotate (???)
        rb.freezeRotation = true;   // how does this not freeze the entire rotation?
        transform.Rotate(rotationThisFrame * Time.deltaTime * UnityEngine.Vector3.forward);
        rb.freezeRotation = false;  // unfreezing rotation so that physics system can take over.
    }
}