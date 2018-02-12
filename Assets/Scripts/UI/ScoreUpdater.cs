using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles updating the score UI.
/// 
/// Author: Mirko Skroch
/// </summary>
public class ScoreUpdater : MonoBehaviour
{

    #region Variable Declarations
    // Visible in Inspector
    [Space]
    [Range(1, 2)]
    [SerializeField] int playerNumber = 1;

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
    TextMeshProUGUI scoreText;

    // State
    int score;
    int previousScore;
    int scalingUp;
    int scalingDown;
    #endregion



    #region Unity Event Functions
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

        originalCounterScale = transform.localScale;
        targetScale = originalCounterScale * (1 + scaleAmount);

        scoreText.text = "0";
    }

    void LateUpdate()
    {
        if (playerNumber == 1) previousScore = ScoreCounter.Instance.Distance1 + ScoreCounter.Instance.Score1;
        else if (playerNumber == 2) previousScore = ScoreCounter.Instance.Distance2 + ScoreCounter.Instance.Score2;
    }
    #endregion



    #region Public Functions
    public void UpdateText()
    {
        if (playerNumber == 1) score = ScoreCounter.Instance.Distance1 + ScoreCounter.Instance.Score1;
        else if (playerNumber == 2) score = ScoreCounter.Instance.Distance2 + ScoreCounter.Instance.Score2;

        if (score != previousScore)
        {
            if (transform.localScale != targetScale && !LeanTween.isTweening(scalingUp))
            {
                // Cancel scaling down and scale up
                LeanTween.cancel(scalingDown);
                scalingUp = LeanTween.scale(gameObject, targetScale, scaleDuration).setEase(LeanTweenType.easeInOutCubic).id;
            }
            // Update the text element with the new distance
            scoreText.text = score.ToString();
        }
        else if (!LeanTween.isTweening(gameObject) && transform.localScale != originalCounterScale)
        {
            // Scale down
            scalingDown = LeanTween.scale(gameObject, originalCounterScale, scaleDuration).setEase(LeanTweenType.easeInOutCubic).id;
        }
    }
    #endregion
}
