using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public float maxThrust;
    public float maxTorque;

    public string spawnObjectTag = "AsteroidMedium";
    public int numberOfSpawnedObjects = 2;

    public int pointsGained;
    public Color popupColor;

    Vector3 thrust;
    Vector3 torque;

    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
        AddForceTorque();
    }

    void AddForceTorque() {
        //Add random thrust and torque to the asteroid
        thrust = new Vector3(Random.Range(-maxThrust, maxThrust), 0, Random.Range(-maxThrust, maxThrust));
        torque = new Vector3(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque));

        rb.AddForce(thrust, ForceMode.Impulse);
        rb.AddTorque(torque, ForceMode.Impulse);
    }

    public void SpawnNewAsteroids() {
        Camera.main.transform.DOShakePosition(0.4f,0.5f);
        //Check if pooled tag is not null, then pool the corresponding objects 'numberOfSpawnedObjects' times
        if (spawnObjectTag != null) {
            for (int i = 0; i < numberOfSpawnedObjects; i++) {
                GameObject newAsteroid = ObjectPooler.instance.GetPooledObject(spawnObjectTag);
                if (newAsteroid != null) {
                    newAsteroid.transform.position = transform.position;
                    newAsteroid.SetActive(true);
                }
            }
        }
        GameObject asteroidParticle = ObjectPooler.instance.GetPooledObject("AsteroidParticle");
        if (asteroidParticle != null) {
            asteroidParticle.transform.position = transform.position;
            asteroidParticle.SetActive(true);
            asteroidParticle.GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "AsteroidLarge" || other.tag == "AsteroidMedium" || other.tag == "AsteroidSmall") {
            rb.Sleep();
            AddForceTorque();
        }
        if(other.tag == "Bullet" || other.tag == "EnemyBullet" || other.tag == "VolleyBullet") {
            //if hit by bullet destroy bullet and if possible create smaller asteroids
            SoundManager.PlaySFX("explosion_asteroid");
            GameManager.instance.AddPoints(pointsGained,transform, popupColor);
            other.gameObject.SetActive(false);
            SpawnNewAsteroids();
            
            gameObject.SetActive(false);
        }
        if(other.tag == "UFO") {
            //if hit UFO destroy it and the asteroid
            CancelInvoke();
            other.gameObject.SetActive(false);
            SpawnNewAsteroids();

            gameObject.SetActive(false);
        }
        if (other.tag == "Player") {
            GameManager.instance.InvokeGameOver();
            Destroy(other.gameObject);
            SpawnNewAsteroids();
            gameObject.SetActive(false);
        }
        if (other.tag == "Shield") {
            rb.Sleep();
            AddForceTorque();
        }
    }

}
