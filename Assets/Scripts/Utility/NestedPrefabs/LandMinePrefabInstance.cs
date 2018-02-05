using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows our Nested LandMine Prefabs to be instantiated with custom parameters, which can be set in the inspector.
/// 
/// Author: Mirko Skroch
/// </summary>
public class LandMinePrefabInstance : PrefabInstance
{

    #region Variable Declarations
    [Header("Explosion Stats")]
    [SerializeField] float explosionForce = 3000;
    [SerializeField] float explosionRadius = 5;
    [SerializeField] float movementBlockTimeOnHit = 10;

    [Header("Camera Shake On Hit")]
    [SerializeField] float magnitude = 2f;
    [SerializeField] float roughness = 10f;
    [SerializeField] float fadeOutTime = 3f;
    #endregion



    #region Public Functions
    public override void BakeInstance()
    {
        base.BakeInstance();

        go.transform.Find("Mesh").GetComponent<LandMineController>().explosionForce = explosionForce;
        go.transform.Find("Mesh").GetComponent<LandMineController>().explosionRadius = explosionRadius;
        go.transform.Find("Mesh").GetComponent<LandMineController>().movementBlockTimeOnHit = movementBlockTimeOnHit;

        go.transform.Find("Mesh").GetComponent<LandMineController>().magnitude = magnitude;
        go.transform.Find("Mesh").GetComponent<LandMineController>().roughness = roughness;
        go.transform.Find("Mesh").GetComponent<LandMineController>().fadeOutTime = fadeOutTime;
    }
    #endregion
}
