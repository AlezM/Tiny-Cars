using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour {

    public VehicleMotor motor;

	void Start () {
        motor = GetComponent<VehicleMotor>();
        if (motor != null)
            motor.SetSpeed(1 + 0.5f * (Random.value - 0.5f));
        else
            Debug.LogError("Motor didn't find!");
	}

	void Update () {
        if (Mathf.Abs(transform.position.y) > 100)
            Destroy(gameObject);
	}
}
