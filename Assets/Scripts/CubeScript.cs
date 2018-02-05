using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// Author: Melanie Ramsch, Mirko Skroch
/// </summary>
public class CubeScript : MonoBehaviour {

    //Variables 
    [Header("Movement")]
    public float moveSpeed = 10;
    public float maxSpeed = 20;
    [Tooltip("Dampening of the cube movement. 0 = no input possible, 1 = normal move input")]
    public float moveDampening = 1;
    public AnimationCurve moveDampeningFadeIn = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Rigidbody rb;
    
    // variables for speedy achievement
    private float speedLimit;
    [HideInInspector]
    public float speedDuration;
    [HideInInspector]
    public bool speedyStarted = false;

    // respawn
    private Vector3 startPosition;
    private Quaternion startRot;


	void Start () {
        rb = GetComponent<Rigidbody>();
        startPosition = rb.transform.position;
        startRot = rb.transform.rotation;

        speedLimit = ScoreCounter.Instance.speedLimit;
	}

	void Update () {
        MoveCube();

        // Check the player speed and trigger Speedy Gonzalez event, if player is fast enough
        if (!speedyStarted && rb.velocity.x + rb.velocity.z >= speedLimit) {
            speedDuration += Time.deltaTime;
            if (speedDuration >= ScoreCounter.Instance.speedDuration) {
                ScoreCounter.Instance.Speedy(rb);
                speedyStarted = true;
            }
        }

        if( rb.position.y <= -10 ) Respawn();
	}



    private void MoveCube () {
        if (Input.GetAxis(Constants.INPUT_HORIZONTAL) != 0 ) {
            rb.AddForce(Vector3.right * Input.GetAxis(Constants.INPUT_HORIZONTAL) * moveSpeed * moveDampening * 60 * Time.deltaTime);
        }
        if (Input.GetAxis(Constants.INPUT_VERTICAL) != 0 ) {
            rb.AddForce(Vector3.forward * Input.GetAxis(Constants.INPUT_VERTICAL) * moveSpeed * moveDampening * 60 * Time.deltaTime);
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void Respawn() {
        rb.position = startPosition;
        rb.rotation = startRot;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        LevelGenerator.Instance.NewLevel();
        speedDuration = 0;

        ScoreCounter.Instance.RespawnTriggered();
    }

    public void BlockMovement(float seconds) {
        moveDampening = 0;
        StartCoroutine(ResetMoveDampeningCoroutine(seconds));
    }



    IEnumerator ResetMoveDampeningCoroutine(float fadeSpeed) {
        for (float i = 0; i < 1; i += 0.0125f * fadeSpeed * 60 * Time.deltaTime) {
            moveDampening = moveDampeningFadeIn.Evaluate(i);
            yield return null;
        }
        moveDampening = 1;
    }
}
