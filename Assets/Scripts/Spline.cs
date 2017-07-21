using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplinePoint {
	Vector3 p1;
	Vector3 p2;
}

public class Spline {

	Vector3 GetPoint( Vector3[] pts, float t ) {
		    float omt = 1f-t;
		    float omt2 = omt * omt;
		    float t2 = t * t;
		    return pts[0] * ( omt2 * omt ) +
			       pts[1] * ( 3f * omt2 * t ) +
			       pts[2] * ( 3f * omt * t2 ) +
			       pts[3] * ( t2 * t );
	}

	Vector3 GetTangent( Vector3[] pts, float t ) {
		float omt = 1f-t;
		float omt2 = omt * omt;
		float t2 = t * t;
		Vector3 tangent = 
			            pts[0] * ( -omt2 ) +
			            pts[1] * ( 3 * omt2 - 2 * omt ) +
			            pts[2] * ( -3 * t2 + 2 * t ) +
			            pts[3] * ( t2 );
		return tangent.normalized;
	}

}