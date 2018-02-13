using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// 
/// Author: Melanie Ramsch
/// </summary>
public class UIController : MonoBehaviour {

	private void Awake() {
	}

	public void QuitCubeRoll() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
	}

	public void GotoMenu() {
		SceneManager.LoadScene(Data.menuScene);
	}

	public void GotoCredits() {
		SceneManager.LoadScene(Data.creditScene);
	}

	public void GotoSingelPlayerScene() {
		SceneManager.LoadScene(Data.singlePlayerScene);
	}


	public void GotoMultiPlayerScene() {
		//SceneManager.LoadScene(Data.XX, LoadSceneMode.Single);
		print("Load Multi Player Scene");
	}

	public void SaveScore() {
		GameObject.Find(Constants.HIGHSCORE_CONTROLLER).GetComponent<HighScoreController>().AddNewEntry();
	}

}
