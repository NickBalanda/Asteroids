using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float lifeTime = 2;
    public GameObject particleRing;

    public GameObject mesh;

    SphereCollider coll;
    TrailRenderer trail;
    Rigidbody rb;

    void Start () {
        coll = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        Destroy(gameObject, lifeTime);
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "AsteroidLarge") {
            other.GetComponent<Asteroid>().SpawnNewAsteroids();
            other.gameObject.SetActive(false);
            GameManager.instance.AddPoints(25);
            MissileDestroyed();
        }
        if (other.tag == "AsteroidMedium") {
            other.GetComponent<Asteroid>().SpawnNewAsteroids();
            other.gameObject.SetActive(false);
            GameManager.instance.AddPoints(50);
            MissileDestroyed();
        }
        if (other.tag == "AsteroidSmall") {
            other.GetComponent<Asteroid>().SpawnNewAsteroids();
            other.gameObject.SetActive(false);
            
            GameManager.instance.AddPoints(100);
            MissileDestroyed();
        }
        if (other.tag == "UFO") {
            GameManager.instance.AddPoints(150);
            other.gameObject.SetActive(false);
            MissileDestroyed();
        }

    }
    void MissileDestroyed() {
        coll.radius = 6.76f;
        particleRing.SetActive(true);
        mesh.SetActive(false);
        rb.Sleep();
        trail.enabled = false;
        Destroy(gameObject,2);
    }
}
