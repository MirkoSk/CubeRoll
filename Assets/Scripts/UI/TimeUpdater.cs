using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 
/// </summary>
public class TimeUpdater : MonoBehaviour 
{

    #region Variable Declarations
    MultiPlayerTimeController timeController;
    TextMeshProUGUI timeText;
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
        timeController = GameObject.FindObjectOfType<MultiPlayerTimeController>();
        timeText = transform.Find("Number").GetComponent<TextMeshProUGUI>();
	}
	
	private void Update () 
	{
        timeText.text = Mathf.CeilToInt(timeController.RestTime).ToString();
	}
	#endregion
}
