using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    //Varriables
    private Vector3 offset;
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Cube");
        offset = transform.position-player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;
    }
}
