using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball;
    public Transform cam;
    public float ballDistance = 2f;
    public float ballForceMin = 150f;
    public float ballForceMax = 400f;
    
    public float totalTimer = 3f;
    public float currentTime = 0.0f;
    public float ballForce = 250f;
    public bool holdingBall = true;
    Rigidbody ballRB;

    bool isPickableBall = false;
    public CanvasController canvasScript;
    public LayerMask pickableLayer;
    RaycastHit hit;

    void Start()
    {
        ballRB = ball.GetComponent<Rigidbody>();
        ballRB.useGravity = false;
        canvasScript.OcualtarCursor(true);
        canvasScript.ActivarSlider(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingBall == true)
        {
            if (Input.GetMouseButtonDown(0)) {
                currentTime = 0.0f;
                canvasScript.SetValueBar(0);
                canvasScript.ActivarSlider(true);
            }
            if (Input.GetMouseButton(0)) {
                currentTime += Time.deltaTime;
                ballForce = Mathf.Lerp(ballForceMin, ballForceMax, currentTime / totalTimer);
                canvasScript.SetValueBar(currentTime / totalTimer);
            }
            if (Input.GetMouseButtonUp(0))
            {
                holdingBall = false;
                ballRB.useGravity = true;
                ballRB.AddForce(cam.forward * ballForce);
                canvasScript.OcualtarCursor(false);
                canvasScript.ActivarSlider(false);
            }
        }
        else {
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, pickableLayer)) {
                if (isPickableBall == false) {
                    isPickableBall = true;
                    canvasScript.ChangePickableBallColor(true);
                }
                if (isPickableBall && Input.GetKeyDown(KeyCode.E)) {
                    holdingBall = true;
                    ballRB.useGravity = false;
                    ballRB.velocity = Vector3.zero;
                    ball.transform.localRotation = Quaternion.identity;
                    GameController.instance.canScore = false;
                    canvasScript.OcualtarCursor(true);


                }
            } else if (isPickableBall == true) {
                isPickableBall = false;
                canvasScript.ChangePickableBallColor(false);
            }
        }
        
        
    }

    private void LateUpdate()
    {
        if (holdingBall == true)
        {
            ball.transform.position = cam.position + cam.forward * ballDistance;
        }
    }
}
