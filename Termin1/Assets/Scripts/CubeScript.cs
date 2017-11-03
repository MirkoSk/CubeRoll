using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour {

    //Variables 
    public int number=42;
    Rigidbody rb;
    public int rollforce=10;

    
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
        if( Input.GetKey(KeyCode.D) ) {
            rb.AddForce(Vector3.right*rollforce);
        }
        if( Input.GetKey(KeyCode.A) ) {
            rb.AddForce(Vector3.right * -rollforce);
        }
        if( Input.GetKey(KeyCode.W) ) {
            rb.AddForce(Vector3.forward * rollforce);
        }
        if( Input.GetKey(KeyCode.S) ) {
            rb.AddForce(Vector3.forward * -rollforce);
        }
    }
    private void respawn() {
        rb.position = startPosition;
        rb.rotation = startRot;
        rb.velocity = Vector3.zero;
    }
}
