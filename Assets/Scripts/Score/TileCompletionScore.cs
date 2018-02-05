using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that gets attached to a trigger to call the TileCompleted method on the ScoreCounter when a player enters it.
/// 
/// Author: Mirko Skroch
/// </summary>
public class TileCompletionScore : MonoBehaviour {

    private bool tilesetCompleted = false;



    void OnTriggerEnter(Collider hit) {
        if (!tilesetCompleted && hit.tag.Contains(Constants.TAG_PLAYER)) {
            ScoreCounter.Instance.TileCompleted();
            tilesetCompleted = true;
        }
    }
}
