using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAI : MonoBehaviour {

	public Vector3 targetPosition;
	public bool forwardBackward = true;
	public bool targetReached = false;

	VehicleMotor motor;
	Transform vehiclePivot;
	int movingDiraction = 1; // Forward = 1, backward = -1;
	RoadManager roadManager;
	public RoadLine currentLine;

	void Start () {
		Debug.Log("Hello creator!");

		motor = GetComponent<VehicleMotor> ();
		if (transform.GetChild (0) != null)
			vehiclePivot = transform.GetChild (0);
		else
			vehiclePivot = transform;

	//	roadManager = RoadManager.Instance ();

		targetPosition = currentLine.end;
	}

	void FixedUpdate () {
		if (!targetReached) {
			TargetController ();
			Movement ();
			Rotation ();
		}
	}

	void TargetController () {
		targetPosition = currentLine.NearestPoint (vehiclePivot.position, 1);
	}

	void Movement () {
		float distance = Vector2.Distance (vehiclePivot.position, currentLine.end); //Vector3.Distance (targetPosition, vehiclePivot.position);
		float speed = 1; //TODO

		movingDiraction = (Vector3.Angle(transform.up, (targetPosition - vehiclePivot.position).normalized) < 110 )? 1 : -1;
		if (distance > 0.2f) {
			motor.SetSpeed (speed * movingDiraction);
			targetReached = false;
		} 
		else {
			motor.SetSpeed (0);	
			targetReached = true;
		}
	}

	void Rotation() {
		Vector3 targetDiraction = (targetPosition - vehiclePivot.position).normalized;
		float absAngle = Vector3.Angle (targetDiraction, transform.up * movingDiraction); 		// Angle absolute value

		if (absAngle > 0.1f) {
			float angle = 
				Mathf.Sign(Vector3.Cross(targetDiraction, transform.up * movingDiraction).z) *	// Angle diraction
				Mathf.Clamp01(absAngle / 45f) *													// Angle absolute value			
				(forwardBackward ? 1 : -1);
			
			motor.SetRotation(angle);
		}
		else {
			motor.SetRotation(0);
		}
	}

	void OnDrawGizmos () {
		if (vehiclePivot == null)
			return;
		
		if (Vector3.Distance (targetPosition, vehiclePivot.position) > 0.2f)
			Gizmos.color = Color.red;
		else
			Gizmos.color = Color.green;		

		Gizmos.DrawSphere (targetPosition, 0.1f);

		Gizmos.color = Color.green;
		Gizmos.DrawLine (vehiclePivot.position, targetPosition);
	}
}
