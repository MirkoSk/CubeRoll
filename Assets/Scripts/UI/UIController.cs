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

	public void GotoMenu(){
		SceneManager.LoadScene(Data.menuScene,LoadSceneMode.Single);
	}

	public void GotoCredits() {
		SceneManager.LoadScene(Data.creditScene,LoadSceneMode.Single);
	}

	public void GotoSingelPlayerScene(){
		SceneManager.LoadScene(Data.singlePlayerScene, LoadSceneMode.Single);
	}


	public void GotoMultiPlayerScene() {
		//SceneManager.LoadScene(Data.XX, LoadSceneMode.Single);
		print("Load Multi Player Scene");
	}
}
