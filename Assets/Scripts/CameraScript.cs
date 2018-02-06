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
    [Range(1, 2)]
    public int playerToFollow = 1;

    Vector3 offset;
    GameObject player;



	void Start () {
        GameObject[] players = GameObject.FindGameObjectsWithTag(Constants.TAG_PLAYER);
        foreach (GameObject go in players)
        {
            if (go.GetComponent<CubeController>().playerNumber == playerToFollow)
            {
                player = go;
            }
        }
        offset = transform.position - player.transform.position;
	}
	
	void Update () {
        transform.position = player.transform.position + offset;
    }
}
