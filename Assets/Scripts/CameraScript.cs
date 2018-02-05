using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// Author: Melanie Ramsch
/// </summary>
public class CameraScript : MonoBehaviour {

    //Varriables
    private Vector3 offset;
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER);
        offset = transform.position-player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;
    }
}
