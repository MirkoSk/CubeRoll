using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers effects and sound when the cube hits the ground.
/// 
/// Author: Mirko Skroch
/// </summary>
[RequireComponent(typeof(CubeController))]
public class CubeGroundHit : MonoBehaviour {

    #region Variable Declarations
    public GameObject cubeGroundHitPSPrefab;

    private Transform dynamicObjectsParent;
    private CubeController cubeScript;
    #endregion



    #region Unity Event Functions
    private void Start() {
        cubeScript = GetComponent<CubeController>();
        dynamicObjectsParent = GameObject.FindGameObjectWithTag(Constants.TAG_DYNAMIC_OBJECTS_PARENT).transform;
    }

    private void OnCollisionEnter(Collision collision) {
        if (cubeScript.Respawning) return;

        for (int i = 0; i < collision.contacts.Length; i++) {
            Instantiate(cubeGroundHitPSPrefab, collision.contacts[i].point + Vector3.up * 0.3f, Quaternion.identity, dynamicObjectsParent);
            AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_HIT);
        }
    }
    #endregion
}
