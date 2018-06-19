using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour {

    public float thrust;
    public float turnThrust;

    float thrustInput;
    float turnInput;

    Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	
	void Update () {
        thrustInput = Input.GetAxis("Vertical") * thrust;
        turnInput = Input.GetAxis("Horizontal") * turnThrust;
    }

    void FixedUpdate() {
        rb.AddRelativeForce(Vector3.forward * thrustInput);
        rb.AddTorque(new Vector3(0, turnInput, 0));
    }

    
}
