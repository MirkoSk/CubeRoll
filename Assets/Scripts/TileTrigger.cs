using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// Author: Melanie Ramsch
/// </summary>
[RequireComponent(typeof(Collider))]
public class TileTrigger : MonoBehaviour
{

	private void OnTriggerEnter (Collider hit)
    {
        // Since Triggers don't understand compound colliders: Go up the hierarchy until you hit the gameObject with the rigidbody
        Rigidbody parent = hit.FindComponentInParents<Rigidbody>();

        if (parent.tag == Constants.TAG_PLAYER) { 
            //calls Method to add a new Tile
            LevelGenerator.Instance.NewTile(transform.parent.position);
            TileDelete();
            //Disable the Trigger
            this.enabled = false;
        }
    }

    private void TileDelete()
    {
        //Counts completed Tiles 
        LevelGenerator.Instance.SetCompletedTiles(LevelGenerator.Instance.GetCompletedTiles() + 1);
        //removes Tiles, which are too old
        if( LevelGenerator.Instance.GetCompletedTiles() > LevelGenerator.TILES_STARTCOUNT ) {
            LevelGenerator.Instance.DeleteOldTile();
            // Checks if StartArea is still an instantiated, and deletes it
            if( GameObject.FindGameObjectWithTag(Constants.TAG_START_AREA) )
                Destroy(GameObject.FindGameObjectWithTag(Constants.TAG_START_AREA));
        }
       
    }
}
