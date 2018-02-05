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



    #region Public Functions
    public override void BakeInstance()
    {
        base.BakeInstance();

        go.transform.Find("Mesh").GetComponent<JumpPadController>().forceAmount = forceAmount;
        go.transform.Find("Mesh").GetComponent<JumpPadController>().forceDuration = forceDuration;
    }
    #endregion
}
