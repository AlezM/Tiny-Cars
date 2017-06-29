using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadCreator))]
public class RoadCreatorEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoadCreator myScript = (RoadCreator)target;
        if (GUILayout.Button("Create Road")) {
            myScript.CreateRoad();
        }
    }
}
