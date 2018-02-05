using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Keeps track of the distance the cube has travelled so far and updates the GUI accordingly.
/// 
/// Author: Mirko Skroch
/// </summary>
public class DistanceCounter : MonoBehaviour {

    #region Variable Declarations
    public GameObject cube;

    private TextMeshProUGUI distanceText;
    [HideInInspector]
    public int distance;
    #endregion



    #region Unity Event Functions
    // Use this for initialization
    void Start () {
        distanceText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        if (cube.transform.position.z > 0)
        {
            distance = (int) cube.transform.position.z;
        }
        else {
            distance = 0;
        }
        distanceText.text = distance.ToString();
    }
    #endregion
}
