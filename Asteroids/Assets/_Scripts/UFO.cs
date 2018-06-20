using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour {

    public float speed;
    public float bulletSpeed;

    Rigidbody rb;
    GameObject player;
    Vector3 direction;

    void Start() {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        //change the direction of movement and shoot every few seconds
        InvokeRepeating("Movement", 1, Random.Range(2, 3));
    }
    void OnEnabled () {       
        InvokeRepeating("Movement", 1, Random.Range(2,3));
    }
    private void OnDisable() {
        CancelInvoke();
    }

    private void Update() {
        direction = (player.transform.position - transform.position).normalized;    
    }

    void Movement() {

        rb.velocity = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized * speed;
        Shoot();
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
    }
}
