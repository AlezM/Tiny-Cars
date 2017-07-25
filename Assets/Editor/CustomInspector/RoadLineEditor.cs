using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

[CustomEditor(typeof(RoadLine))]
public class RoadLineEditor : Editor {

	RoadLine roadLine;
	LineRenderer lineRenderer;

#region Initalization / Destruction

	[MenuItem("GameObject/Create Other/Road Line %#r")]
	public static void init() {
		GameObject go = new GameObject();
		go.name = "Road Line";
		RoadLine rl = go.AddComponent<RoadLine> ();
		LineRenderer lr = go.AddComponent<LineRenderer> ();

		lr.SetPosition (0, rl.start);
		lr.SetPosition (1, rl.end);

		Selection.activeObject = go;
	}

	public void OnEnable() {
		roadLine = (RoadLine)target;
		lineRenderer = roadLine.GetComponent<LineRenderer>();
	}
#endregion

#region Scene

	void OnSceneGUI () {
		float size = HandleUtility.GetHandleSize (roadLine.transform.position);
		roadLine.start = roadLine.transform.position;

		Handles.color = Color.green;
		Handles.DrawLine (roadLine.start, roadLine.end);
		Handles.DrawCube (0, roadLine.start, Quaternion.identity, 0.2f * size);
		Handles.color = Color.blue;
		Handles.DrawCube (0, roadLine.end, Quaternion.identity, 0.2f * size);

		Handles.color = Color.black;
		Handles.ArrowHandleCap (0, roadLine.start, Quaternion.LookRotation (roadLine.end - roadLine.start), 0.6f * size, EventType.Repaint);

		roadLine.end = Handles.PositionHandle (roadLine.end, Quaternion.identity);

		lineRenderer.SetPosition (0, roadLine.start);
		lineRenderer.SetPosition (1, roadLine.end);
	}

#endregion

}