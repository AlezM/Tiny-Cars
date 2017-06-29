using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoadCreator : MonoBehaviour {

    [Header("Prefabs")]
    public float spriteSize = 1.1f;
    public GameObject mainRoadPrefab;
    public GameObject dotedRoadMarking;

    [Header("Settings")]
    public int roadLength = 1;
    [Range(1, 5)]
    public int roadWidth = 1;

    public void CreateRoad() {
        //Clean up!!
        for (int i = 1; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i));
        }

        //Create new!!
        GameObject roadDetail_GO = (GameObject)Instantiate(mainRoadPrefab, this.transform);
        roadDetail_GO.GetComponent<SpriteRenderer>().size = new Vector2(spriteSize * (2 * roadWidth + 1), spriteSize * roadLength);

        for (int i = 1; i < roadWidth; i++) {
            InstantiateMarking(i);
        }
    }

    void InstantiateMarking(int i) {
        //Right side.
        GameObject roadDetail_GO = (GameObject)Instantiate(dotedRoadMarking, this.transform);
        roadDetail_GO.transform.position = new Vector3(i * spriteSize, 0, 0);
        roadDetail_GO.GetComponent<SpriteRenderer>().size = new Vector2(roadDetail_GO.GetComponent<SpriteRenderer>().size.x, spriteSize * roadLength);
        roadDetail_GO.name = "RightLine" + i.ToString();

        //Left side.
        roadDetail_GO = (GameObject)Instantiate(dotedRoadMarking, this.transform);
        roadDetail_GO.transform.position = new Vector3(-i * spriteSize, 0, 0);
        roadDetail_GO.GetComponent<SpriteRenderer>().size = new Vector2(roadDetail_GO.GetComponent<SpriteRenderer>().size.x, spriteSize * roadLength);
        roadDetail_GO.name = "LeftLine" + i.ToString();
    }
}
