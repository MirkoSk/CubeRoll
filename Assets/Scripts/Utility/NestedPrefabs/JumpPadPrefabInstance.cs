using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows our Nested JumpPad Prefabs to be instantiated with custom parameters, which can be set in the inspector.
/// 
/// Author: Mirko Skroch
/// </summary>
public class JumpPadPrefabInstance : PrefabInstance
{

    #region Variable Declarations
    [SerializeField] float forceAmount = 15f;
    [SerializeField] float forceDuration = 1f;
    #endregion



    #region Protected Functions
    protected override void SpawnPrefab()
    {
        base.SpawnPrefab();

        prefabInstance.transform.Find("Mesh").GetComponent<JumpPadController>().forceAmount = forceAmount;
        prefabInstance.transform.Find("Mesh").GetComponent<JumpPadController>().forceDuration = forceDuration;
    }
    #endregion
}
