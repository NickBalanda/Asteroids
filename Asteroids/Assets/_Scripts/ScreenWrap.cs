using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {

    public float screenTopOffset;
    public float screenBottomOffset;
    public float screenRightOffset;
    public float screenLeftOffset;

    //Sides of the screen
    float screenTop;
    float screenBottom;
    float screenRight;
    float screenLeft;

    private void Start() {
        
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        screenTop = vertExtent + screenTopOffset;
        screenBottom = -vertExtent + screenBottomOffset;
        screenRight = horzExtent + screenRightOffset;
        screenLeft = -horzExtent + screenLeftOffset;
    }
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
