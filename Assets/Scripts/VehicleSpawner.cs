using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour {

    [Header("Prefabs")]
    public GameObject[] vehiclePrefabs;

    [Header("Settings")]
    public float spawnDelay = 3f;
    public float spawnTimeRate = 1f;

    public Transform vehicleContainer;

    [HideInInspector]
	public RoadLine[] roadLines = null;

	private static VehicleSpawner vehicleSpawner;

	public static VehicleSpawner Instance () {
		if (!vehicleSpawner) {
			vehicleSpawner = (VehicleSpawner)FindObjectOfType (typeof(VehicleSpawner));
			if (!vehicleSpawner)
				Debug.LogWarning ("VehicleSpawner does not exist!!!");
		}

		return vehicleSpawner;
	}

	public void SetSpawnPoints (RoadLine[] lines) {
		roadLines = lines;
	}

	public void StartSpawn (float time) {
		if (roadLines == null) {
			Debug.LogError ("SpawnPoints is NULL!!!");
			return;
		}
		CancelInvoke("SpawnVehicle");
		InvokeRepeating("SpawnVehicle", 0, time);
	}

	public void CancelSpawn () {
		CancelInvoke("SpawnVehicle");
	}

    void SpawnVehicle() {
		if (roadLines == null) {
			Debug.LogError ("SpawnPoints is NULL!!!");
			CancelInvoke ("SpawnVehicle");
		}
			
		if (vehiclePrefabs == null || vehiclePrefabs.Length < 1) {
			Debug.LogError ("There are not vehicle prefabs to spawn!!!");
			return;
		}

		int spawnPointIndex = Random.Range (0, roadLines.Length);
		GameObject vehicle_GO = (GameObject) Instantiate (
			vehiclePrefabs [Random.Range (0, vehiclePrefabs.Length)],
			roadLines [spawnPointIndex].start,
			((spawnPointIndex + 1 > roadLines.Length / 2) ? Quaternion.Euler (0, 0, 0) : Quaternion.Euler (0, 0, 180)),
			vehicleContainer
		);
	
	//	vehicle_GO.GetComponent<VehicleAI> ().SetUp (roadLines[], spawnPointIndex);	
    }
}
