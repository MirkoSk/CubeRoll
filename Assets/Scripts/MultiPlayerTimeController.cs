using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiPlayerTimeController : MonoBehaviour {

	private float duration = 60f;
	private float timeToEndLevel = 0;

	private void Start() {
		timeToEndLevel = Time.time + duration;
	}

	private void Update() {
		if (Time.time> timeToEndLevel){
			SceneManager.LoadScene(Constants.MULTIPLAYER_SCORE_SCENE);
		}
	}

}
