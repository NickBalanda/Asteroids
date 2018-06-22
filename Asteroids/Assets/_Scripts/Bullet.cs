using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float lifeTime = 2;

	// Use this for initialization
	void OnEnable () {
        StartCoroutine(DestroyAfterTime());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "AsteroidLarge") {
            GameManager.instance.AddPoints(25);
        }
        if (other.tag == "AsteroidMedium") {
            GameManager.instance.AddPoints(50);
        }
        if (other.tag == "AsteroidSmall") {
            GameManager.instance.AddPoints(100); 
        }
        if (other.tag == "UFO" && gameObject.tag != "EnemyBullet") {
            GameManager.instance.AddPoints(150);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        if (other.tag == "Player" && gameObject.tag == "EnemyBullet") {
            GameManager.instance.InvokeGameOver();
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
        if (other.tag == "Shield" && gameObject.tag == "EnemyBullet") {
            gameObject.SetActive(false);
        }
    }
}
