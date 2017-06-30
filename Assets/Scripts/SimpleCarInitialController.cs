using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarInitialController : MonoBehaviour {

	public float forwardSpeed = 10f;
	public float backwardSpeed = 3f;
	public float rotationSpeed = 10f;

    Vector2 touchBeginPos = Vector2.zero;

	void Update () {
		float hInput = Input.GetAxis ("Horizontal");
		float vInput = Input.GetAxis ("Vertical");

        if (Input.touchCount > 0) {
            hInput = (Input.GetTouch(0).deltaPosition.x > 0.1f) ? 1 : 0;
            vInput = (Input.GetTouch(0).deltaPosition.y > 0.1f) ? 1 : 0;
        }

      /*if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                touchBeginPos = Input.GetTouch(0).position;

            if (Input.touchCount > 1) {
                hInput = 0.1f * (touchBeginPos.x - Input.GetTouch(0).position.x);
                vInput = 0.1f * (touchBeginPos.y - Input.GetTouch(0).position.y);
            }
        }*/

        transform.Translate ( transform.up * vInput * ((vInput > 0)? forwardSpeed: backwardSpeed) * 0.01f * (1 - 0.2f * Mathf.Abs(hInput)), Space.World);
		transform.Rotate (Vector3.back * hInput * vInput * rotationSpeed);
	}
}
