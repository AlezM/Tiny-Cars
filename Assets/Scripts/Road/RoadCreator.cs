using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoadCreator : MonoBehaviour {

    [Header("Prefabs")]
    public float spriteX = 1.1f;
    public float spriteY = 1.1f;
    public GameObject mainRoadPrefab;
    public GameObject middleRoadPrefab;
    public GameObject lineRoadMarking;
    public GameObject dotedRoadMarking;
    public GameObject sideRoadPrefab;

    [Header("Settings")]
    public Transform detailsContainer;
    [Range(1, 100)]
    public int roadLength = 1;
    [Range(1, 10)]
    public int roadWidth = 1;

    Vector3[] spawnPoints;

    public void Start() {
        CreateSpawns();
    //    FindObjectOfType<CarSpawner>().spawnPoints = spawnPoints;
    }

    public void CreateRoad() {
        for (int i = 0; i < 5; i++)
            CleanUp();


        //Spawns
        CreateSpawns();

        //Graphics
        GameObject roadDetail_GO = (GameObject)Instantiate(mainRoadPrefab, detailsContainer);
        roadDetail_GO.GetComponent<SpriteRenderer>().size = new Vector2(spriteX * (2 * roadWidth + 1), spriteY * roadLength);
        roadDetail_GO.name = "MainRoad";

        roadDetail_GO = (GameObject)Instantiate(middleRoadPrefab, detailsContainer);
        roadDetail_GO.GetComponent<SpriteRenderer>().size = new Vector2(spriteX, spriteY * roadLength);
        roadDetail_GO.name = "MainRoad";

        for (int i = 1; i <= roadWidth; i++) {
            InstantiateMarking(i);           
        }
    }


    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (spawnPoints == null)
            return;

        for (int i = 0; i < spawnPoints.Length; i++) {
            Gizmos.DrawSphere(spawnPoints[i], 0.1f);
        }
    }
    

    public void CleanUp() {
        for (int i = 0; i < detailsContainer.childCount; i++)
        {
            DestroyImmediate(detailsContainer.GetChild(i).gameObject);
        }
        spawnPoints = new Vector3[0];
    }

    void InstantiateMarking(int i) {
        //Right side.
        GameObject instance = dotedRoadMarking;
        if (i == roadWidth)
            instance = lineRoadMarking;

        GameObject roadDetail_GO = (GameObject)Instantiate(instance, detailsContainer);
        roadDetail_GO.transform.position = new Vector3(i * spriteX + 0.5f, 0, 0);
        roadDetail_GO.GetComponent<SpriteRenderer>().size = new Vector2(roadDetail_GO.GetComponent<SpriteRenderer>().size.x, spriteY * roadLength);
        roadDetail_GO.name = "RightLine" + i.ToString();

        //Left side.
        roadDetail_GO = (GameObject)Instantiate(instance, detailsContainer);
        roadDetail_GO.transform.position = new Vector3(-i * spriteX - 0.5f, 0, 0);
        roadDetail_GO.GetComponent<SpriteRenderer>().size = new Vector2(roadDetail_GO.GetComponent<SpriteRenderer>().size.x, spriteY * roadLength);
        roadDetail_GO.name = "LeftLine" + i.ToString();
    }


    //Нумерация идет с права на лево, левая полоса - самый большой индекс;
    void CreateSpawns() {
        spawnPoints = new Vector3[roadWidth * 2];
        for (int i = 0; i < roadWidth; i++) {
            spawnPoints[i] = new Vector3( -((i + 1) * spriteX - 0.05f), roadLength * spriteY / 2, 0 );              //Left
            spawnPoints[2*roadWidth - i - 1] = new Vector3((i + 1) * spriteX - 0.05f, -roadLength * spriteY / 2, 0);  //Right
        }
    }
}
