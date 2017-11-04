using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour {

    public GameObject cube;

    private Text distanceText;
    [HideInInspector]
    public int distance;

	// Use this for initialization
	void Start () {
        distanceText = transform.GetChild(0).GetComponent<Text>();
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
}
