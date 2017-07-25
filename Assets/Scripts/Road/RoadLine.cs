using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLine : MonoBehaviour {
	public Vector2 start;
	public Vector2 end;

	public RoadLine () {
		start = Vector2.zero;
		end = Vector2.zero;
	}

	public RoadLine (Vector2 _start, Vector2 _end) {
		start = _start;
		end = _end;
	}

	public float Distance (Vector3 other) {
		return (
			Mathf.Abs ((end.y - start.y) * other.x - (end.x - start.x) * other.y + end.x * start.y - end.y * start.x) /
			Mathf.Sqrt ((end.y - start.y) * (end.y - start.y) + (end.x - start.x) * (end.x - start.x))
		);
	}

	public Vector3 NearestPoint (Vector3 other, float offset = 0) {
		float a = Distance (other);
		float c = Vector2.Distance (other, start);
		float b = Mathf.Sqrt (c * c - a * a);
		Vector2 dir1 = (end - start).normalized;
		Vector2 dir2 = ( new Vector2(other.x, other.y) - start ).normalized;

		int sign = (Vector2.Angle(dir1, dir2) <= 90) ? 1 : -1;

		Vector3 point = start + sign * dir1 * (b + offset);
		return point;
	}
}