﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {

    public float bulletSpeed;

    public float fireRate = 0.5f;
    float currentFireTime;

    public Transform shootPosition;

    void Start () {
        currentFireTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        currentFireTime += Time.deltaTime;
        if (Input.GetButton("Fire1") && currentFireTime >= fireRate) {
            Shoot();
            currentFireTime = 0;
        }
    }

    void Shoot() {
        SoundManager.PlaySFX("PlayerLaser");
        GameObject bullet = ObjectPooler.instance.GetPooledObject("Bullet");
        if (bullet != null) {
            bullet.transform.position = shootPosition.position;
            bullet.transform.rotation = shootPosition.rotation;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            bullet.SetActive(true);
        }
    }
}
