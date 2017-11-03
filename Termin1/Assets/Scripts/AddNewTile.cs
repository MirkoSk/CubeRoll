using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNewTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
    }
    private void OnTriggerEnter (Collider other) {
        transform.Find("StartArea").GetComponent<LevelGenerator>().addNewTile(this.transform.position,this.transform.rotation);
    }
}
