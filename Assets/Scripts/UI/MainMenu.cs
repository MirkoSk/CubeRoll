using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Initializing things for the mainMenu scene.
/// 
/// Author: Mirko Skroch
/// </summary>
public class MainMenu : MonoBehaviour 
{
	
	#region Variable Declarations
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
		DisplayHeighestScoreInMenu();

		if (!AudioManager.Instance.GetAudioSource(Constants.SOUND_MAINMENU).isPlaying)
        {
            AudioManager.Instance.StopAllMusic();
            AudioManager.Instance.PlaySound(Constants.SOUND_MAINMENU);
        }

#if !UNITY_EDITOR
        // Show cursor in built game
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
#endif
    }
	#endregion

	#region PrivateFunctions
	//@Melanie Ramsch
	private void DisplayHeighestScoreInMenu(){
		Text display = GameObject.Find("GamesHighestScore").GetComponent<Text>();
		if(getHighestScorePoints() != "0") display.text = "Highest Score by "+getHighestScoreName()+"\n"+"With   "+getHighestScorePoints()+"   Points!";
		else display.text = "";
	}
	private string getHighestScorePoints() {
		if(PlayerPrefs.HasKey("points0")) return PlayerPrefs.GetInt("points0").ToString();
		else return "0";
	}
	private string getHighestScoreName() {
		if(PlayerPrefs.HasKey("name0")) return PlayerPrefs.GetString("name0").ToString();
		else return "0";
	}
	#endregion
}
