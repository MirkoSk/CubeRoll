using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that scales the attached GameObject up and down in an endless loop.
/// 
/// Author: Mirko Skroch
/// </summary>
public class Pulsate : MonoBehaviour 
{

    #region Variable Declarations
    [Space]
    [Tooltip("Scale amount on change of the distance counter in percent.")]
    [Range(0f, 0.3f)]
    [SerializeField]
    float scaleAmount = 0.1f;
    [Tooltip("Scale duration on change of the distance counter in seconds.")]
    [Range(0f, 1f)]
    [SerializeField]
    float scaleDuration = 0.3f;
    #endregion



    #region Unity Event Functions
    private void Start () 
	{
        LeanTween.scale(gameObject, transform.localScale * (1 + scaleAmount), scaleDuration).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();	
	}
    #endregion
}
