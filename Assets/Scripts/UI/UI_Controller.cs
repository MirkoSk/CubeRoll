using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour {

	private void Awake() {
	}

	public void quitCubeRoll() {
	#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
	#else
         Application.Quit();
	#endif
	}

	public void gotoMenu(){
		SceneManager.LoadScene(Data.menuScene,LoadSceneMode.Single);
	}

	public void gotoCredits() {
		SceneManager.LoadScene(Data.creditScene,LoadSceneMode.Single);
	}

	public void gotoSingelPlayerScene(){
		SceneManager.LoadScene(Data.singlePlayerScene, LoadSceneMode.Single);
	}


	public void gotoMultiPlayerScene() {
		//SceneManager.LoadScene(Data.XX, LoadSceneMode.Single);
		print("Load Multi Player Scene");
	}
}
