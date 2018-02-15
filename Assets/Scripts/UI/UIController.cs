using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contains all methods used via Button-Click
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
		SceneManager.LoadScene(Constants.MENU_SCENE);
	}

	public void GotoCredits() {
		SceneManager.LoadScene(Constants.CREDIT_SCENE);
	}

	public void GotoSingelPlayerScene() {
		SceneManager.LoadScene(Constants.SINGLEPLAYER_SCENE);
	}


	public void GotoMultiPlayerScene() {
		SceneManager.LoadScene(Constants.MULTIPLAYER_SCENE, LoadSceneMode.Single);
	}

	public void SaveScore() {
		GameObject.Find(Constants.HIGHSCORE_CONTROLLER).GetComponent<HighScoreController>().AddNewEntry();
	}

    public void PlayConfirmSound() {
        AudioManager.Instance.PlayMenuConfirm();
    }

}
