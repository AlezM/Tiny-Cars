using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMotor : MonoBehaviour {

    public float forwardSpeed = 10f;
    public float backwardSpeed = 4f;
    public float rotationSpeed = 7f;

	public float movingAcceleration = 8f;
	public float rotationAcceleration = 15f;

    public bool usePhysic = false;

    float targetSpeed = 0;
    float targetRotation = 0;

    float hInput = 0;
    float vInput = 0;

    Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
            usePhysic = false;
    }

    void FixedUpdate()
    {
		vInput = Mathf.Lerp(vInput, targetSpeed, movingAcceleration * Time.deltaTime);
		hInput = Mathf.Lerp(hInput, targetRotation, rotationAcceleration * Time.deltaTime);

        if (!usePhysic) 
            transform.Translate(transform.up * vInput * ((vInput > 0) ? forwardSpeed : backwardSpeed) * 0.01f * (1 - 0.2f * Mathf.Abs(hInput)), Space.World);
        else
            rb2D.velocity = transform.up * vInput * ((vInput > 0) ? forwardSpeed : backwardSpeed) * (1 - 0.2f * Mathf.Abs(hInput)) / 2;
		
		transform.Rotate(Vector3.back * hInput * vInput * rotationSpeed);
    }
    public void SetSpeed(float speed) {
		targetSpeed = Mathf.Clamp(speed, -1f, 1f);
    }

    public void SetRotation(float rotation) {
		targetRotation = Mathf.Clamp(rotation, -1f, 1f);
    }

    public float GetSpeed () {
        return targetSpeed * forwardSpeed;
    }
}
