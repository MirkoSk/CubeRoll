using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCompletionScore : MonoBehaviour {

    private bool tilesetCompleted = false;

    void OnTriggerEnter(Collider hit) {
        if (!tilesetCompleted && hit.tag.Contains("Player")) {
            ScoreCounter.Instance.tileCompleted();
            tilesetCompleted = true;
        }
    }
}
