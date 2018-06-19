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
}
