using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour {

	private void OnTriggerEnter (Collider other) {
        if( other.tag == "Player" ) { 
            //calls Method to add a new Tile
            LevelGenerator.Instance.newTile(transform.parent.position);
            tileDelete();
            //Disable the Trigger
            this.enabled = false;
        }
    }

    private void tileDelete() {
        //Counts completed Tiles 
        LevelGenerator.Instance.setCompletedTiles(LevelGenerator.Instance.getCompletedTiles() + 1);
        //removes Tiles, which are too old
        if( LevelGenerator.Instance.getCompletedTiles() > LevelGenerator.TILES_STARTCOUNT ) {
            LevelGenerator.Instance.deleteOldTile();
            // Checks if StartArea is still an instantiated, and deletes it
            if( GameObject.FindGameObjectWithTag("StartArea") )
                Destroy(GameObject.FindGameObjectWithTag("StartArea"));
        }
       
    }
}
