using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float lifeTime = 2;

	void OnEnable () {
        StartCoroutine(DestroyAfterTime());
	}

    IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "AsteroidLarge") {
            GameManager.instance.AddPoints(25,other.transform,Color.blue);
        }
        if (other.tag == "AsteroidMedium") {
            GameManager.instance.AddPoints(50, other.transform, Color.red);
        }
        if (other.tag == "AsteroidSmall") {
            GameManager.instance.AddPoints(100, other.transform, Color.green); 
        }
        if (other.tag == "UFO" && gameObject.tag != "EnemyBullet") {
            GameManager.instance.AddPoints(150, other.transform, Color.yellow);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        if (other.tag == "Player" && gameObject.tag == "EnemyBullet") {
            Instantiate(GameManager.instance.playerExplosion, other.transform.position, Quaternion.identity);
            GameManager.instance.InvokeGameOver();
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
        if (other.tag == "Shield" && gameObject.tag == "EnemyBullet") {
            gameObject.SetActive(false);
        }
    }
}
