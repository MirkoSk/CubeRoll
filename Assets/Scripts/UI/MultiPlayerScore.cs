using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayerScore : MonoBehaviour {

	Text winner,winnerScore,loser,loserScore;
	
	
	void Start () {
		winner = GameObject.Find("Winner").GetComponent<Text>();
		winnerScore = GameObject.Find("WinnerScore").GetComponent<Text>();
		loser = GameObject.Find("Loser").GetComponent<Text>();
		loserScore = GameObject.Find("LoserScore").GetComponent<Text>();
		setText();
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
}
