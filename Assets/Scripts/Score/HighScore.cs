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

	public void SaveHighScore(string name, int score) {
		List<Score> HighScores = new List<Score>();

		
		for ( int i = 1; i<= scoreBoardlength && PlayerPrefs.HasKey("HighScore" + i + "score"); i++){ 
			Score temp = new Score();
			temp.score = PlayerPrefs.GetInt("HighScore" + i + "score");
			temp.name = PlayerPrefs.GetString("HighScore" + i + "name");
			HighScores.Add(temp);
		}
		if(HighScores.Count == 0) {
			Score temp = new Score();
			temp.name = name;
			temp.score = score;
			HighScores.Add(temp);
		} else {
			for(int i = 1; i <= HighScores.Count && i <= scoreBoardlength; i++) {
				if(score > HighScores[i - 1].score) {
					Score temp = new Score();
					temp.name = name;
					temp.score = score;
					HighScores.Insert(i - 1, temp);
					break;
				}
				if(i == HighScores.Count && i < scoreBoardlength) {
					Score temp = new Score();
					temp.name = name;
					temp.score = score;
					HighScores.Add(temp);
					break;
				}
			}
		}

		for (int i =1; i <= scoreBoardlength && i <= HighScores.Count; i++)
		while(i <= scoreBoardlength && i <= HighScores.Count) {
			PlayerPrefs.SetString("HighScore" + i + "name", HighScores[i - 1].name);
			PlayerPrefs.SetInt("HighScore" + i + "score", HighScores[i - 1].score);
		}

	}

	public List<Score> GetHighScore() {
		List<Score> HighScores = new List<Score>();

		int i = 1;
		while(i <= scoreBoardlength && PlayerPrefs.HasKey("HighScore" + i + "score")) {
			Score temp = new Score();
			temp.score = PlayerPrefs.GetInt("HighScore" + i + "score");
			temp.name = PlayerPrefs.GetString("HighScore" + i + "name");
			HighScores.Add(temp);
			i++;
		}

		return HighScores;
	}

	public void ClearLeaderBoard() {
		//for(int i=0;i<HighScores.
		List<Score> HighScores = GetHighScore();

		for(int i = 1; i <= HighScores.Count; i++) {
			PlayerPrefs.DeleteKey("HighScore" + i + "name");
			PlayerPrefs.DeleteKey("HighScore" + i + "score");
		}
	}

	void OnApplicationQuit() {
		PlayerPrefs.Save();
	}
}



