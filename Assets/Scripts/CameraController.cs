using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float moveScale = 9f;
    public float zoomScale = 0.1f;
    public float smoothSpeed = 1f;

    Camera myCam;

    Vector3 mousePrevPos = Vector3.zero;
    Vector3 camPrevPos = Vector3.zero;
    Vector3 deservedCameraPosition;
    float pevZoom = 0;
    float deservedZoom;

    void Start() {
        myCam = GetComponent<Camera>();
        deservedCameraPosition = transform.position;
        deservedZoom = myCam.orthographicSize;
        camPrevPos = transform.position;
    }

    void LateUpdate () {

#if UNITY_ANDROID || UNITY_IPHONE
        if (Input.touchCount == 1) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                mousePrevPos = Input.GetTouch(0).position;
                camPrevPos = transform.position;
            }
            deservedCameraPosition = camPrevPos - moveScale * myCam.orthographicSize * 
                (new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y) - mousePrevPos) / Screen.width;
        }

        if (Input.touchCount >= 2) {
            if (Input.GetTouch(1).phase == TouchPhase.Began) {
                pevZoom = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            deservedZoom = zoomScale * Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) - pevZoom;
        }
#endif

#if UNITY_EDITOR
        if (Input.GetMouseButton(0)) {
            if (Input.GetMouseButtonDown(0)) {
                mousePrevPos = Input.mousePosition;
                camPrevPos = transform.position;
            }
            deservedCameraPosition = camPrevPos - moveScale * myCam.orthographicSize * (Input.mousePosition - mousePrevPos) / Screen.width;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            deservedZoom -= zoomScale * Input.mouseScrollDelta.y;
            if (deservedZoom < 2)
                deservedZoom = 2;
            if (deservedZoom > 13)
                deservedZoom = 13;
        }
#endif
        
        transform.position = Vector3.Lerp(transform.position, deservedCameraPosition, Time.fixedDeltaTime * smoothSpeed);
        myCam.orthographicSize = Mathf.Lerp(myCam.orthographicSize, deservedZoom, Time.deltaTime * smoothSpeed);
    }
}
