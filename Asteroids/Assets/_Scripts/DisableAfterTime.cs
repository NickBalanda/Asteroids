using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour {
    public float time;

    void Start() {
        StartCoroutine(DisableObject());    
    }
    IEnumerator DisableObject() {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
