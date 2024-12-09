using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float thrustStrength = 100f;

    Rigidbody rb;

    void OnEnable() 
    {
        thrust.Enable();
    }

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate() 
    {
        if(thrust.IsPressed())
        {
            rb.AddRelativeForce((Vector3.up * thrustStrength) * Time.fixedDeltaTime);
        }
    }
}
