using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour {

    public float speed;
    public float bulletSpeed;
    public float changeRateMin;
    public float changeRateMax;

    float nextChange;

    Rigidbody rb;
    GameObject player;
    Vector3 direction;

    void Start() {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        Movement();
    }

    private void FixedUpdate() {
        //Get players direction
        if(player != null)
            direction = (player.transform.position - transform.position).normalized;

        //change the direction of movement and shoot every few seconds
        if (Time.time >= nextChange)
        {
            nextChange = Time.time + Random.Range(changeRateMin,changeRateMax);
            Movement();
            Shoot();
        }
    }

    void Movement() {
        rb.velocity = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized * speed;
    }

    void Shoot() {
        GameObject bullet = ObjectPooler.instance.GetPooledObject("EnemyBullet");
        if (bullet != null) {
            bullet.transform.position = transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
            bullet.transform.rotation = q;
            bullet.GetComponent<Rigidbody>().velocity = direction.normalized * bulletSpeed;
            bullet.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "AsteroidLarge" || other.tag == "AsteroidMedium" || other.tag == "AsteroidSmall") {
            //if hit UFO destroy it and the asteroid
            CancelInvoke();
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<Asteroid>().SpawnNewAsteroids();

            gameObject.SetActive(false);
        }
        if (other.tag == "Bullet") {
            //if hit by bullet destroy bullet and ufo
            CancelInvoke();
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        if (other.tag == "Player") {
            GameManager.instance.GameOver();
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
