using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Loads and Saves Highscore List in HighScore Scene. 
/// Adds new Entry to Highscore List 
/// 
/// Author: Melanie Ramsch
/// </summary>
public class HighScoreController: MonoBehaviour {

	private const int scoreBoardlength = 10;
	private List<HighScoreEntry> highScoreList;

	#region UnityFunctions
	void Awake() {
		DisplayPlayerScore();
	}

	private void Update() {
		//Debugging Purpouses... [ToDo] Delete Later
		/*if ( Input.GetKeyDown(KeyCode.Backspace)){
			DeleteHighScoreList();
		}*/
	}
	
	private void Start() {
		highScoreList = new List<HighScoreEntry>();
		FillHighScoreList();
		DisplayHighScoreList();

        AudioManager.Instance.PlaySound(Constants.SOUND_NEW_HIGHSCORE);
    }
	#endregion

	#region Public Functions
	public void AddNewEntry() {
		HighScoreEntry newEntry = new HighScoreEntry(GetPlayerName(), GetPlayerPoints());
		bool inserted = false;
		for( int i =0; i<highScoreList.Count;i++){
			if (highScoreList[i].PointsSmallerThan(newEntry.GetPoints())){
				highScoreList.Insert(i, newEntry);
				inserted = true;
				break;
			}
		}
		if(!inserted) highScoreList.Add(newEntry);
		DisplayHighScoreList();
		SaveHighScoreList();
	}
	#endregion

	#region Private Functions
	
	private void FillHighScoreList(){
		for ( int i = 0; i< scoreBoardlength; i++){
			if(PlayerPrefs.HasKey("name" + i)) {
				string name = PlayerPrefs.GetString("name" + i);
				int points = PlayerPrefs.GetInt("points" + i);
				HighScoreEntry entry = new HighScoreEntry(name, points);
				highScoreList.Add(entry);
			} else break;
		}
	}

	private void DisplayHighScoreList(){
		string nameOutput = "";
		string pointsOutput = "";

		for ( int i = 0; i<highScoreList.Count && i < scoreBoardlength; i++){
			nameOutput += highScoreList[i].GetName() + "\n";
			pointsOutput += highScoreList[i].GetPoints().ToString() + "\n";
		}
		GameObject.Find("HighScoreNames").GetComponent<Text>().text = nameOutput;
		GameObject.Find("HighScorePoints").GetComponent<Text>().text = pointsOutput;
	}

	private void SaveHighScoreList() {
		for(int i = 0; i < highScoreList.Count && i < scoreBoardlength; i++) {
			PlayerPrefs.SetString("name" + i, highScoreList[i].GetName());
			PlayerPrefs.SetInt("points" + i, highScoreList[i].GetPoints());
		}
	}

	private void DeleteHighScoreList(){
		GameObject.Find("HighScoreNames").GetComponent<Text>().text = "";
		GameObject.Find("HighScorePoints").GetComponent<Text>().text = "";
		highScoreList.Clear();
		PlayerPrefs.DeleteAll();
	}

	private int GetPlayerPoints(){
		return Data.singlePlayerScore;
	}
	private string GetPlayerName(){
		string inputText = GameObject.Find("InputName").GetComponentInChildren<Text>().text;
		if(inputText.Equals("")) return "Anonymous";
		else return inputText;
	}
	#endregion



	#region HighScoreUI
	private void DisplayPlayerScore(){
		GameObject.Find("PlayerScoreNumber").GetComponent<Text>().text = GetPlayerPoints().ToString();
	}
	#endregion
}



