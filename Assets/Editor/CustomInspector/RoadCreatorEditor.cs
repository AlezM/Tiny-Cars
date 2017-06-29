using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadCreator))]
public class RoadCreatorEditor : Editor {

    bool automaticUpdate = true;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        automaticUpdate = GUILayout.Toggle(automaticUpdate, "Automatic Update");

        RoadCreator myScript = (RoadCreator)target;
        if (GUILayout.Button("Create Road") || automaticUpdate) {
            myScript.CreateRoad();
        }

        if (GUILayout.Button("Clean up!"))
        {
            for (int i = 0; i < 5; i++)
                myScript.CleanUp();
        }
    }

    
}
