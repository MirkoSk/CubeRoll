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
    [Tooltip("The timer will warn the players that the game is coming to an end at this rest time.")]
    [SerializeField] int warnTime = 30;
    [Tooltip("Scale amount on change of the score counter in percent.")]
    [Range(0f, 0.3f)]
    [SerializeField]
    float scaleAmount = 0.1f;
    [Tooltip("Scale duration on change of the score counter in seconds.")]
    [Range(0f, 1f)]
    [SerializeField]
    float scaleDuration = 0.3f;
    [SerializeField] Gradient colorChangeOverLifetime;

    // Private Variables
    // Configuration
    Vector3 originalCounterScale;
    Vector3 targetScale;
    MultiPlayerTimeController timeController;
    TextMeshProUGUI timeText;

    // State
    int restTime;
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
        timeController = GameObject.FindObjectOfType<MultiPlayerTimeController>();
        timeText = transform.Find("Number").GetComponent<TextMeshProUGUI>();

        restTime = Mathf.CeilToInt(timeController.RestTime);
        originalCounterScale = transform.localScale;
        targetScale = originalCounterScale * (1 + scaleAmount);
    }
	
	private void Update () 
	{
        int newRestTime = Mathf.CeilToInt(timeController.RestTime);
        if (newRestTime != restTime)
        {
            // set new restTime
            restTime = newRestTime;
            timeText.text = restTime.ToString();

            // Change the color
            timeText.color = colorChangeOverLifetime.Evaluate(restTime / timeController.Duration);

            // Tween it!
            if (restTime == warnTime)
            {
                AudioManager.Instance.PlaySound(Constants.SOUND_TIMER_WARNING);
                LeanTween.scale(timeText.gameObject, targetScale * 1.2f, 0.2f).setEase(LeanTweenType.easeInOutCubic).setLoopPingPong(2);
            }
            else LeanTween.scale(timeText.gameObject, targetScale, scaleDuration).setEase(LeanTweenType.punch);
        }
	}
	#endregion
}
