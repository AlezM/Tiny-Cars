using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarInitialController : MonoBehaviour {

	public float forwardSpeed = 10f;
	public float backwardSpeed = 3f;
	public float rotationSpeed = 10f;

	void Start () {
		
	}
	
	void Update () {
		float hInput = Input.GetAxis ("Horizontal");
		float vInput = Input.GetAxis ("Vertical");

		transform.Translate ( transform.up * vInput * ((vInput > 0)? forwardSpeed: backwardSpeed) * 0.01f * (1 - 0.2f * Mathf.Abs(hInput)), Space.World);
		transform.Rotate (Vector3.back * hInput * vInput * rotationSpeed);
	}
}
