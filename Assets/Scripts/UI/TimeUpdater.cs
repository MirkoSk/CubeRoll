using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles updating the time UI for the Multiplayer.
/// 
/// Author: Mirko Skroch
/// </summary>
public class TimeUpdater : MonoBehaviour 
{

    #region Variable Declarations
    // Visible in Inspector
    [Space]
    [Tooltip("Scale amount on change of the score counter in percent.")]
    [Range(0f, 0.3f)]
    [SerializeField]
    float scaleAmount = 0.1f;
    [Tooltip("Scale duration on change of the score counter in seconds.")]
    [Range(0f, 1f)]
    [SerializeField]
    float scaleDuration = 0.3f;

    // Private Variables
    // Configuration
    Vector3 originalCounterScale;
    Vector3 targetScale;
    MultiPlayerTimeController timeController;
    TextMeshProUGUI timeText;

    // State
    int currentRestTime;
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
        timeController = GameObject.FindObjectOfType<MultiPlayerTimeController>();
        timeText = transform.Find("Number").GetComponent<TextMeshProUGUI>();

        currentRestTime = Mathf.CeilToInt(timeController.RestTime);
        originalCounterScale = transform.localScale;
        targetScale = originalCounterScale * (1 + scaleAmount);
    }
	
	private void Update () 
	{
        int restTime = Mathf.CeilToInt(timeController.RestTime);
        if (restTime != currentRestTime) {
            currentRestTime = restTime;
            timeText.text = currentRestTime.ToString();
            LeanTween.scale(timeText.gameObject, targetScale, scaleDuration).setEase(LeanTweenType.punch);
        }
	}
	#endregion
}
