using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour {

    public VehicleMotor motor;
    public float forwardRayOffset = 1f;

	void Start () {
        motor = GetComponent<VehicleMotor>();
        if (motor != null)
            motor.SetSpeed(1 - 0.25f * Random.value);
        else
            Debug.LogError("Motor didn't find!");
	}

	void Update () {
        if (Mathf.Abs(transform.position.y) > 100)
            Destroy(gameObject);
	}

    void FixedUpdate () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * forwardRayOffset, transform.up, 1f);
        //RaycastHit2D hit = Physics2D.BoxCast(transform.position + transform.up * 1.25f, new Vector2(1f, 0.25f), 0, transform.up);
        if (hit.collider != null) {
            if (hit.distance < 0.8f) {
                if (hit.distance > 0.5f)
                    motor.SetSpeed(hit.distance);
                else
                    motor.SetSpeed(0);
            }
            else
            {
                VehicleMotor vM = hit.transform.GetComponent<VehicleMotor>();
                if (vM != null)
                {
                    float s = vM.GetSpeed() / motor.forwardSpeed;
                    motor.SetSpeed(s);
                }
            }
        }
        else {
            motor.SetSpeed(1 - 0.25f * Random.value);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + transform.up * forwardRayOffset,
           transform.position + transform.up * (forwardRayOffset + 1f));
    }
}
