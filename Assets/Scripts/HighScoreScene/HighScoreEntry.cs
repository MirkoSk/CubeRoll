using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Represents an Entry in the Highscore List
/// 
/// Author: Melanie Ramsch
/// </summary>
public class HighScoreEntry {
	string name;
	int points;

	public HighScoreEntry ( string name, int points){
		this.name = name;
		this.points = points;
	}

	public string GetName(){
		return name;
	}

	public int GetPoints(){
		return points;
	}

	public bool PointsSmallerThan(int other){
		if(points < other) return true;
		else return false;
	}

}
