using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 1000f;

    Rigidbody rb;

    void OnEnable() 
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
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
        }
    }
}
