using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour {

	private void OnTriggerEnter (Collider other) {
        if( other.tag == "Player" ) { 
            //calls Method to add a new Tile
            LevelGenerator.Instance.newTile(transform.parent.position);
            //Disable the Trigger
            this.enabled = false;
        }
    }
}
