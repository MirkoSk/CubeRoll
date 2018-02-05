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
    [Space]
    [Tooltip("Scale amount on change of the score counter in percent.")]
    [Range(0f, 0.3f)]
    [SerializeField]
    float scaleAmount = 0.1f;
    [Tooltip("Scale duration on change of the score counter in seconds.")]
    [Range(0f, 1f)]
    [SerializeField]
    float scaleDuration = 0.3f;

    TextMeshProUGUI scoreText;
    int previousScore;
    Vector3 originalCounterScale;
    Vector3 targetScale;
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
        previousScore = ScoreCounter.Instance.Distance + ScoreCounter.Instance.Score;
    }
    #endregion



    #region Public Functions
    public void UpdateText()
    {
        if (ScoreCounter.Instance.Distance + ScoreCounter.Instance.Score != previousScore)
        {
            if (transform.localScale != targetScale && !LeanTween.isTweening(scalingUp))
            {
                // Cancel scaling down and scale up
                LeanTween.cancel(scalingDown);
                scalingUp = LeanTween.scale(gameObject, targetScale, scaleDuration).setEase(LeanTweenType.easeInOutCubic).id;
            }
            // Update the text element with the new distance
            scoreText.text = (ScoreCounter.Instance.Distance + ScoreCounter.Instance.Score).ToString();
        }
        else if (!LeanTween.isTweening(gameObject) && transform.localScale != originalCounterScale)
        {
            // Scale down
            scalingDown = LeanTween.scale(gameObject, originalCounterScale, scaleDuration).setEase(LeanTweenType.easeInOutCubic).id;
        }
    }
    #endregion
}
