using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {

	private static HighScore Instance;
	private const int scoreBoardlength = 10;

	#region Class Definitions
	public class Score {
		public int score;
		public string name;
	}
	#endregion

	void Awake() {
		if(Instance == null) {
			Instance = this;
		} else if(Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	
}



