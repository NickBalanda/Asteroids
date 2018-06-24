using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour {

    public float thrust;
    public float Rotation_Smoothness;

    public ParticleSystem engineParticle;


    Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
	}
		
	void Update () {
        if (Input.GetButton("Fire2")) {
            rb.AddRelativeForce(Vector3.forward * thrust);
        }
        if (Input.GetButtonDown("Fire2")) {
            engineParticle.Play();
        }
        if (Input.GetButtonUp("Fire2")) {
            engineParticle.Stop();
        }
        RotateWithMouse();
    }

    //Creates a ray from the cursor that intersects a plane, that point will be the point to look at 
    void RotateWithMouse() {
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;
      
        if (playerPlane.Raycast(ray, out hitdist)) {
            
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Rotation_Smoothness * Time.deltaTime);
        }
    }

}
