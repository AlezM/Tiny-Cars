using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 10f;
    public Vector3 offset = new Vector3(0, 0, -10);

    void Start () {
        if (target == null)
            Debug.LogWarning("Target is NULL.");
    }

    void Update () {
        if (target == null)
            return;

        Vector3 deservedPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, deservedPosition, smoothSpeed * Time.deltaTime);

    }
}
