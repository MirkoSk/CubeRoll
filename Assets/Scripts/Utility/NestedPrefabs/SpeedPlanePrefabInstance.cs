using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows our Nested SpeedPlane Prefabs to be instantiated with custom parameters, which can be set in the inspector.
/// 
/// Author: Mirko Skroch
/// </summary>
public class SpeedPlanePrefabInstance : PrefabInstance
{

    #region Variable Declarations
    [SerializeField] float force = 50f;
    #endregion

    

    #region Protected Functions
    protected override void SpawnPrefab()
    {
        base.SpawnPrefab();

        prefabInstance.transform.Find("Mesh").GetComponent<SpeedPlaneController>().force = force;
    }
    #endregion
}
