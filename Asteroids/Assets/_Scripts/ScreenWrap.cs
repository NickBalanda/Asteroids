using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {

    public float screenTop = 12;
    public float screenBottom = -12;
    public float screenRight = 18;
    public float screenLeft = -18;

    private void FixedUpdate() {

        Vector3 newPos = transform.position;

        if(transform.position.z > screenTop) {
            newPos.z = screenBottom;
        }
        if(transform.position.z < screenBottom) {
            newPos.z = screenTop;
        }
        if (transform.position.x > screenRight) {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft) {
            newPos.x = screenRight;
        }

        transform.position = newPos;
    }
}
