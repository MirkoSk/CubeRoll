﻿using System.Collections;
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

    TextMeshProUGUI highscoreText;
    ParticleSystem highscoreParticleSystem;
    Vector3 originalCounterScale;
    #endregion



    #region Unity Event Functions
    private void Start () 
	{
        highscoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        highscoreParticleSystem = transform.GetChild(1).GetComponent<ParticleSystem>();

        originalCounterScale = transform.localScale;
	}
	#endregion
	
	
	
	#region Public Functions
	public void UpdateText()
    {
        highscoreParticleSystem.Play();
        highscoreText.text = ScoreCounter.Instance.Highscore.ToString();
        LeanTween.scale(gameObject, transform.localScale * (1 + scaleAmount), scaleDuration * (1f / 3.5f)).setEase(LeanTweenType.easeOutBack).setOnComplete(() => {
            LeanTween.scale(gameObject, transform.localScale * (1 - scaleAmount), scaleDuration * (1.5f / 3.5f)).setEase(animationCurve).setLoopCount(2).setOnComplete(() =>
            {
                LeanTween.scale(gameObject, originalCounterScale, scaleDuration * (1f / 3.5f)).setEase(LeanTweenType.easeInOutSine);
            });
        });

    }
	#endregion
}
