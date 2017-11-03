using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour {

    //Variables 
    [Header("Movement")]
    public float moveSpeed = 10;
    public float maxSpeed = 20;
    [Tooltip("Dampening of the cube movement. 0 = no input possible, 1 = normal move input")]
    public float moveDampening = 1;

    [Space]
    [Header("Other")]
    public AnimationCurve fadeIn = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Rigidbody rb;

    
    // respawn
    private Vector3 startPosition;
    private Quaternion startRot;
   

	void Start () {
        rb = GetComponent<Rigidbody>();
        startPosition = rb.transform.position;
        startRot = rb.transform.rotation;
	}
	
	void Update () {
        moveCube();
        if( rb.position.y <= -10 ) respawn();
	}

    private void moveCube () {
        if( Input.GetAxis("Horizontal") != 0 ) {
            rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * moveDampening * 60 * Time.deltaTime);
        }
        if( Input.GetAxis("Vertical") != 0 ) {
            rb.AddForce(Vector3.forward * Input.GetAxis("Vertical") * moveSpeed * moveDampening * 60 * Time.deltaTime);
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void respawn() {
        rb.position = startPosition;
        rb.rotation = startRot;
        rb.velocity = Vector3.zero;
    }

    public void blockMovement(float seconds) {
        moveDampening = 0;
        StartCoroutine(dampenMovementCoroutine(seconds));
    }

    IEnumerator dampenMovementCoroutine(float fadeSpeed) {
        for (float i = 0; i < 1; i += 0.0125f * fadeSpeed * 60 * Time.deltaTime) {
            moveDampening = fadeIn.Evaluate(i);
            yield return null;
        }
        moveDampening = 1;
    }
}
