using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour {

    public VehicleMotor motor;
    public float forwardRayOffset = 1f;
    Vector3[] roadLines;
    public int myRoadLine;
    Vector3 roadDirection;

    float waitForChangeLine = 2f;
    float wfclTimer;
    public bool changeLine = false;

    void Start () {
        motor = GetComponent<VehicleMotor>();
        if (motor != null)
            motor.SetSpeed(1 - 0.25f * Random.value);
        else
            Debug.LogError("Motor didn't find!");

        if (roadLines == null)
        {
            roadLines = new Vector3[1];
            roadLines[0] = transform.position;
            myRoadLine = 0;
        }
        roadDirection = (transform.position.y > 0) ? Vector3.down : Vector3.up;

        //Timers 
        wfclTimer = waitForChangeLine;
    }

    void Update () {
        if (transform.position.magnitude > 50)
            Destroy(gameObject);
	}

    void FixedUpdate () {
        Movement();
        Rotation();

        if (changeLine) {
            ChangeRoadLine();
            changeLine = false;
        }
    }

    void OnDrawGizmos () {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + transform.up * forwardRayOffset,
           transform.position + transform.up * (forwardRayOffset + 1f));
    }

    public void SetUp (Vector3[] _roadLines, int _myRoadLine) {
        roadLines = _roadLines;
        myRoadLine = _myRoadLine;
    }

    void Movement() {
        RaycastHit2D forwardHit = Physics2D.Raycast(transform.position + transform.up * forwardRayOffset, transform.up, 1f);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position + transform.up * forwardRayOffset + transform.right * 0.5f, transform.up, 0.5f);
        RaycastHit2D lefttHit = Physics2D.Raycast(transform.position + transform.up * forwardRayOffset - transform.right * 0.5f, transform.up, 0.5f);
        //RaycastforwardHit2D forwardHit = Physics2D.BoxCast(transform.position + transform.up * 1.25f, new Vector2(1f, 0.25f), 0, transform.up);

        if (forwardHit.collider != null || rightHit.collider != null || lefttHit.collider != null)
        {
            if (forwardHit.distance < 0.5f)
            {
                //motor.SetSpeed(0);
            }
            else
            {
                VehicleMotor vm = forwardHit.transform.GetComponent<VehicleMotor>();
                if (vm != null)
                {
                    float s = vm.GetSpeed() / motor.forwardSpeed;
                    motor.SetSpeed(s);
                }
                else
                {
                    motor.SetSpeed(0);
                }
            }
        }
        else
        {
            motor.SetSpeed(1);
        }
    }

    void Rotation() {
        float distance = transform.position.x - roadLines[myRoadLine].x;
        if (Mathf.Abs(distance) > 0.05f)
        {
            Vector3 deservedDiraction = new Vector3(Mathf.Clamp(-distance, -10f, 10f), 5 * roadDirection.y, 0).normalized;
            float angle = Vector3.Angle(deservedDiraction, transform.up);
            float angleDiraction = Mathf.Sign(Vector3.Cross(deservedDiraction, transform.up).z);
            float hInput = angleDiraction * Mathf.Clamp(angle / 180f, 0, 1);

            motor.SetRotation(hInput);
        }
        else
        {
            motor.SetRotation(0);
        }
    }

    void ChangeRoadLine() {
        RaycastHit2D boxHitRight = Physics2D.BoxCast(transform.position + transform.right * 1.1f, new Vector2(1f, 1f), 0, transform.up);
        RaycastHit2D boxHitLeft = Physics2D.BoxCast(transform.position - transform.right * 1.1f, new Vector2(1f, 1f), 0, transform.up);

        bool rightLineClean = (boxHitRight.collider == null);
        bool leftLineClean = (boxHitLeft.collider == null);

        if (rightLineClean || leftLineClean) {
            if (myRoadLine < roadLines.Length/2) {
                if ( (myRoadLine + 1 < roadLines.Length / 2) && leftLineClean ) { 
                    myRoadLine++;
                }
                else if ( (myRoadLine - 1 >= 0) && rightLineClean ) {
                    myRoadLine--;
                }
            }
            else {
                if ((myRoadLine + 1 < roadLines.Length) && leftLineClean) {
                    myRoadLine++;
                }
                else if ((myRoadLine - 1 >= roadLines.Length / 2) && rightLineClean) {
                    myRoadLine--;
                }
            }
        }
    }
}
