using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoadLine : MonoBehaviour {

	public Vector2 start;
	public Vector2 end;

	float Distance (Vector3 other) {
		return (
			Mathf.Abs ((end.y - start.y) * other.x - (end.x - start.x) * other.y + end.x * start.y - end.y * start.x) /
			Mathf.Sqrt ((end.y - start.y) * (end.y - start.y) + (end.x - start.x) * (end.x - start.x))
		);
	}
}
