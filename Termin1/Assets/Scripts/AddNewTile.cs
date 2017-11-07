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
        if( other.tag == "Player" ) { 
            LevelGenerator gen = GameObject.Find("_SCRIPTS").GetComponent<LevelGenerator>();
            gen.addNewTile(transform.parent.position, transform.parent.rotation);
            this.enabled = false;
        }
    }
}
