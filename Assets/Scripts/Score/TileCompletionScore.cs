using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that gets attached to a trigger to call the TileCompleted method on the ScoreCounter when a player enters it.
/// 
/// Author: Mirko Skroch
/// </summary>
[RequireComponent(typeof(Collider))]
public class TileCompletionScore : MonoBehaviour
{
    bool tilesetCompletedPlayer1 = false;
    bool tilesetCompletedPlayer2 = false;



    void OnTriggerEnter(Collider hit)
    {
        // Since Triggers don't understand compound colliders: Go up the hierarchy until you hit the gameObject with the CubeController
        CubeController parent = hit.FindComponentInParents<CubeController>();

        if (parent.tag.Contains(Constants.TAG_PLAYER)) {
            if (parent.PlayerNumber == 1 && !tilesetCompletedPlayer1)
            {
                ScoreCounter.Instance.TileCompleted(parent.PlayerNumber);
                tilesetCompletedPlayer1 = true;
            }
            else if (parent.PlayerNumber == 2 && !tilesetCompletedPlayer2)
            {
                ScoreCounter.Instance.TileCompleted(parent.PlayerNumber);
                tilesetCompletedPlayer2 = true;
            }
        }
    }
}
