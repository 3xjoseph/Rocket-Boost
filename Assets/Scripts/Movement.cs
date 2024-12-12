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
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
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

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(rocketThrust);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
