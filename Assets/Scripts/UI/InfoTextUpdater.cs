using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles updating the InfoTexts.
/// 
/// Author: Mirko Skroch
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class InfoTextUpdater : MonoBehaviour 
{

    #region Variable Declarations
    [SerializeField] float scaleDuration = 0.5f;

    TextMeshProUGUI infoText;
    TextMeshProUGUI infoText2;
    Coroutine hideTextCoroutine;
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
        infoText = GetComponent<TextMeshProUGUI>();
        infoText2 = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
	}
	
	private void Update () 
	{
		
	}
    #endregion



    #region Public Functions
    public void UpdateText(string text1, string text2, float displayDuration)
    {
        if (!text1.Contains("Speedy Gonzalez x"))
        {
            infoText.transform.localScale = Vector3.zero;
            LeanTween.scale(infoText.gameObject, Vector3.one, scaleDuration).setEase(LeanTweenType.easeOutElastic);
        }
        infoText.text = text1;
        infoText2.text = text2;

        if (hideTextCoroutine != null) StopCoroutine(hideTextCoroutine);
        hideTextCoroutine = StartCoroutine(HideText(displayDuration));
    }
    #endregion



    #region Private Functions

    #endregion



    #region Coroutines
    IEnumerator HideText(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        infoText.text = "";
        infoText2.text = "";
        hideTextCoroutine = null;
    }
    #endregion
}
