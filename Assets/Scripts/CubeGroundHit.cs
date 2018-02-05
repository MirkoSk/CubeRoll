using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers effects and sound when the cube hits the ground.
/// 
/// Author: Mirko Skroch
/// </summary>
public class CubeGroundHit : MonoBehaviour {

    public GameObject cubeGroundHitPSPrefab;

    private Transform parent;



    private void Start() {
        parent = GameObject.FindGameObjectWithTag(Constants.TAG_DYNAMIC_OBJECTS_PARENT).transform;
    }

    private void OnCollisionEnter(Collision collision) {
        for (int i = 0; i < collision.contacts.Length; i++) {
            Instantiate(cubeGroundHitPSPrefab, collision.contacts[i].point + Vector3.up * 0.3f, Quaternion.identity, parent);
            AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_HIT);
        }
    }
}
