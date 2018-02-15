using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles Output for Multiplayer Score Scene
/// 
/// Author: Melanie Ramsch
/// </summary>
public class MultiPlayerScore : MonoBehaviour {

	Text winner,winnerScore,loser,loserScore;
	
	
	void Start () {
		winner = GameObject.Find("WinnerName").GetComponent<Text>();
		winnerScore = GameObject.Find("WinnerScore").GetComponent<Text>();
		loser = GameObject.Find("LoserName").GetComponent<Text>();
		loserScore = GameObject.Find("LoserScore").GetComponent<Text>();
		setText();
		playSound();
	}
	
	private void setText(){
		if( Data.player1 >= Data.player2){
			winner.text = "Player 1";
			winnerScore.text = Data.player1.ToString();
			loser.text = "Player 2";
			loserScore.text = Data.player2.ToString();
		}
		else {
			winner.text = "Player 2";
			winnerScore.text = Data.player2.ToString();
			loser.text = "Player 1";
			loserScore.text = Data.player1.ToString();
		}
	}

	void playSound(){
		AudioManager.Instance.StopAllMusic();
		AudioManager.Instance.PlaySound(Constants.SOUND_FIREWORKS);
		AudioManager.Instance.PlaySound(Constants.SOUND_NEW_HIGHSCORE);
	}
}
