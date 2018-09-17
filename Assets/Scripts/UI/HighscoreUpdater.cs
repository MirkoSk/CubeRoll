using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles updating the highscore UI.
/// 
/// Author: Mirko Skroch
/// </summary>
public class HighscoreUpdater : MonoBehaviour 
{

    #region Variable Declarations
    // Visible in Inspector
    [Space]
    [Tooltip("Scale amount on change of the distance counter in percent.")]
    [Range(0f, 0.3f)]
    [SerializeField]
    float scaleAmount = 0.1f;
    [Tooltip("Scale duration on change of the distance counter in seconds.")]
    [Range(0f, 4f)]
    [SerializeField]
    float scaleDuration = 0.3f;
    [SerializeField] AnimationCurve animationCurve;

    // Private Variables
    // Configuration
    TextMeshProUGUI highscoreText;
    Vector3 originalCounterScale;
    #endregion



    #region Unity Event Functions
    private void Start () 
	{
        highscoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (Data.singlePlayerGame) UpdateText();

		originalCounterScale = transform.localScale;
	}
	#endregion
	
	
	
	#region Public Functions
	public void UpdateText()
    {
        highscoreText.text = GetHighestScore();

        LeanTween.scale(gameObject, transform.localScale * (1 + scaleAmount), scaleDuration * (1f / 3.5f)).setEase(LeanTweenType.easeOutBack).setOnComplete(() => {
            LeanTween.scale(gameObject, transform.localScale * (1 - scaleAmount), scaleDuration * (1.5f / 3.5f)).setEase(animationCurve).setLoopCount(2).setOnComplete(() =>
            {
                LeanTween.scale(gameObject, originalCounterScale, scaleDuration * (1f / 3.5f)).setEase(LeanTweenType.easeInOutSine);
            });
        });

    }
	#endregion

	private string GetHighestScore(){
		if(PlayerPrefs.HasKey("name0")) return PlayerPrefs.GetInt("points0").ToString();
		else return "0";
	}
}
