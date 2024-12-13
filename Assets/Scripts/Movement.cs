using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rb;


    [Header("Movement Reference Settings")]
    [Tooltip("Set the input action for the rocket thrust")][SerializeField] public InputAction thrust;
    [Tooltip("Set the input action for the rocket rotation")][SerializeField] public InputAction rotation;
    
    [Header("Movement Strength Settings")]
    [Tooltip("Adjust the strength of the rocket thrust")][SerializeField] float thrustStrength = 1000f;
    [Tooltip("Adjust the strength of the rocket rotation")][SerializeField] float rotationStrength = 1000f;

    [Header("Audio Settings")]
    [Tooltip("Rrocket thrust audio clip")][SerializeField] AudioClip rocketThrust;

    [Header("Rocket Booster Particle Reference Settings")]
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;


    void OnEnable() 
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
            if(!rightBooster.isPlaying)
            {
                leftBooster.Stop();
                rightBooster.Play();
            }
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
            if(!leftBooster.isPlaying)
            {
                rightBooster.Stop();
                leftBooster.Play();
            }
        } 
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }       
    }

    void ApplyRotation(float rotationStrengthParameter)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationStrengthParameter * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce((Vector3.up * thrustStrength) * Time.fixedDeltaTime);
            mainBooster.Play();

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(rocketThrust);
            }

            if(!mainBooster.isPlaying)
            {
                mainBooster.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainBooster.Stop();
        }
    }
}
