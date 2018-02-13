using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Calculates the offset and moves Main Camera
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
		CalculateOffset();
	}
	
	void Update () {
        transform.position = player.transform.position + offset;
    }

	private void CalculateOffset(){
		GameObject[] players = GameObject.FindGameObjectsWithTag(Constants.TAG_PLAYER);
		foreach(GameObject gameObj in players) {
			if(gameObj.GetComponent<CubeController>().PlayerNumber == playerToFollow) {
				player = gameObj;
			}
		}
		offset = transform.position - player.transform.position;
	}
}
