﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to Trigger at the beginning of each tile
/// Triggers new tile
/// Triggers deleting old tiles
/// 
/// Author: Melanie Ramsch
/// </summary>
[RequireComponent(typeof(Collider))]
public class TileTrigger : MonoBehaviour
{
    bool tileTriggered;


	private void OnTriggerEnter (Collider hit)
    {
        if (tileTriggered) return;

        // Since Triggers don't understand compound colliders: Go up the hierarchy until you hit the gameObject with the rigidbody
        Rigidbody parent = hit.FindComponentInParents<Rigidbody>();

        if (parent.tag == Constants.TAG_PLAYER) {
			AddNewTile();
			
			CountCompleteTiles();
			
			if ( Data.singlePlayerGame) TileDelete();

            //Disable the Trigger
            tileTriggered = true;
          
		}
    }

	private void AddNewTile(){
		LevelGenerator.Instance.AddNewTile(transform.parent.position);
	}

	private void CountCompleteTiles() {
		LevelGenerator.Instance.SetCompletedTiles(LevelGenerator.Instance.GetCompletedTiles() + 1);
	}

	private void TileDelete()
    {     
        //removes Tiles, which are too old
        if( LevelGenerator.Instance.GetCompletedTiles() > LevelGenerator.TILES_STARTCOUNT ) {
            LevelGenerator.Instance.DeleteOldTile();
            // Checks if StartArea is still an instantiated, and deletes it
            if( GameObject.FindGameObjectWithTag(Constants.TAG_START_AREA) )
                Destroy(GameObject.FindGameObjectWithTag(Constants.TAG_START_AREA));
        }
       
    }

	
}
