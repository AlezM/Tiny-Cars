using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

    [Header("Prefabs")]
    public GameObject[] vehiclePrefabs;

    [Header("Settings")]
    public float spawnDelay = 3f;
    public float spawnTimeRate = 1f;
    float str;

    public Transform vehicleContainer;

    [HideInInspector]
    public Vector3[] spawnPoints = null;    

    void Start () {
        float str = spawnTimeRate;
        InvokeRepeating("SpawnCar", spawnDelay, spawnTimeRate);
    }

    void SpawnCar() {
        Debug.Log("Hello creator!");

        if (spawnPoints != null) {
            if (vehiclePrefabs == null)
                return;

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject vehicle_GO = (GameObject)Instantiate(
                vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)],
                spawnPoints[spawnPointIndex],
                ((spawnPointIndex + 1 > spawnPoints.Length/2)? Quaternion.Euler(0,0,0): Quaternion.Euler(0, 0, 180)),
                vehicleContainer
            );
            vehicle_GO.GetComponent<SimpleAI>().SetUp(spawnPoints, spawnPointIndex);
        }

        //If timerate was changed, we want to restart our Invoke
        if (spawnTimeRate != str) {
            CancelInvoke("SpawnCar");
            str = spawnTimeRate;
            InvokeRepeating("SpawnCar", 0, spawnTimeRate);
        }
    }
}
