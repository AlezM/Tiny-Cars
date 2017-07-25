using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour {

	class RoadLine {
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
	}

	[Header("Main Settings")]
	[Range(1, 100)]
	public int length = 10;
	[Range(1, 4)]
	public int width = 1;

	[Header("Road Parts Prefabs")]
	public GameObject mainRoadPrefab;
	public GameObject middleSeparatorPrefab;
	public GameObject lineSeparatorPrefab;
	public GameObject roadBorderPrefab;

	[Header("Road Setting")]
	public float lineWidth;
	public float middleSeparatorWidth;

	Transform detailsContainer;

	RoadLine[] rightRoadLines;
	RoadLine[] leftRoadLines;

	private static RoadManager roadManager;

	public static RoadManager Instance () {
		if (!roadManager) {
			roadManager = (RoadManager)FindObjectOfType (typeof(RoadManager));
			if (!roadManager)
				Debug.LogError ("RoadManager does not exist!!!");
		}

		return roadManager;
	}

	void Start() {
		CreateRoadGraphics ();
		CreateRoadWayPoints ();
	}



	void CreateRoadGraphics () {
		detailsContainer = (new GameObject()).transform;
		detailsContainer.position = this.transform.position;
		detailsContainer.parent = this.transform;
		detailsContainer.name = "Road";

		GameObject roadDetail_GO = (GameObject)Instantiate(mainRoadPrefab, detailsContainer, false);
		roadDetail_GO.transform.position = this.transform.position;
		roadDetail_GO.GetComponent<SpriteRenderer> ().size = new Vector2 (2 * width * lineWidth + middleSeparatorWidth, length);

		roadDetail_GO = (GameObject)Instantiate(middleSeparatorPrefab, detailsContainer);
		roadDetail_GO.transform.position = this.transform.position; 
		roadDetail_GO.GetComponent<SpriteRenderer> ().size = new Vector2(middleSeparatorWidth, length);

		if (width > 1) {
			for (int i = 1; i < width; i++) {
				roadDetail_GO = (GameObject)Instantiate(lineSeparatorPrefab, this.transform.position + new Vector3(middleSeparatorWidth/2 + lineWidth * i, 0, 0), Quaternion.identity, detailsContainer);
				roadDetail_GO.GetComponent<SpriteRenderer> ().size = new Vector2 (0.3f, length);
				roadDetail_GO = (GameObject)Instantiate(lineSeparatorPrefab, this.transform.position - new Vector3(middleSeparatorWidth/2 + lineWidth * i, 0, 0), Quaternion.identity, detailsContainer);
				roadDetail_GO.GetComponent<SpriteRenderer> ().size = new Vector2 (0.3f, length);
			}

			roadDetail_GO = (GameObject)Instantiate(roadBorderPrefab, this.transform.position + new Vector3(middleSeparatorWidth/2 + lineWidth * width, 0, 0), Quaternion.identity, detailsContainer);
			roadDetail_GO.GetComponent<SpriteRenderer> ().size = new Vector2 (0.3f, length);
			roadDetail_GO = (GameObject)Instantiate(roadBorderPrefab, this.transform.position - new Vector3(middleSeparatorWidth/2 + lineWidth * width, 0, 0), Quaternion.identity, detailsContainer);
			roadDetail_GO.GetComponent<SpriteRenderer> ().size = new Vector2 (0.3f, length);
		}
	}

	void CreateRoadWayPoints () {
		rightRoadLines = new RoadLine[width];
		leftRoadLines = new RoadLine[width];

		for (int i = 0; i < width; i++) {
			rightRoadLines [i] = new RoadLine(
				this.transform.position - Vector3.up * length / 2 + Vector3.right * ((i + 0.5f) * lineWidth + middleSeparatorWidth / 2),
				this.transform.position + Vector3.up * length / 2 + Vector3.right * ((i + 0.5f) * lineWidth + middleSeparatorWidth / 2)
			);
			leftRoadLines [width - 1 - i] = new RoadLine(
				this.transform.position + Vector3.up * length / 2 - Vector3.right * ((i + 0.5f) * lineWidth + middleSeparatorWidth / 2),
				this.transform.position - Vector3.up * length / 2 - Vector3.right * ((i + 0.5f) * lineWidth + middleSeparatorWidth / 2)
			);
		}
	}

	void OnDrawGizmos () {
		if (rightRoadLines == null || leftRoadLines == null)
			return;

		for (int i = 0; i < rightRoadLines.Length; i++) {
			Gizmos.color = Color.green;
			Gizmos.DrawCube(rightRoadLines [i].start, Vector3.one * 0.5f);
			Gizmos.DrawCube(leftRoadLines [i].start, Vector3.one * 0.5f);
		}
	}
}
