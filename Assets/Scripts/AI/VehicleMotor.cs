using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMotor : MonoBehaviour {

    public float forwardSpeed = 10f;
    public float backwardSpeed = 4f;
    public float rotationSpeed = 7f;

    public bool usePhysic = false;

    float deservedSpeed;

    float hInput = 0;
    float vInput = 1;

    Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
            usePhysic = false;
    }

    void Update()
    {
        vInput = Mathf.Lerp(vInput, deservedSpeed, Time.deltaTime * 8);

        if (!usePhysic) {
            transform.Translate(transform.up * vInput * ((vInput > 0) ? forwardSpeed : backwardSpeed) * 0.01f * (1 - 0.2f * Mathf.Abs(hInput)), Space.World);
            transform.Rotate(Vector3.back * hInput * vInput * rotationSpeed);
        }
        else {
            rb2D.velocity = transform.up * vInput;
        }
    }
    public void SetSpeed(float speed) {
        deservedSpeed = speed;
    }

    public float GetSpeed () {
        return deservedSpeed * forwardSpeed;
    }
}
