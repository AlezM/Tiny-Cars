using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMotor : MonoBehaviour {

    public float forwardSpeed = 10f;
    public float backwardSpeed = 4f;
    public float rotationSpeed = 7f;

    float maxSpeed;

    float hInput;
    float vInput;

    void Update () {
        vInput = Mathf.Lerp(vInput, maxSpeed, Time.deltaTime * 8);

        transform.Translate(transform.up * vInput * ((vInput > 0) ? forwardSpeed : backwardSpeed) * 0.01f * (1 - 0.2f * Mathf.Abs(hInput)), Space.World);
        transform.Rotate(Vector3.back * hInput * vInput * rotationSpeed);
    }

    public void SetSpeed(float speed) {
        maxSpeed = speed;
    }

    public float GetSpeed () {
        return maxSpeed * forwardSpeed;
    }
}
