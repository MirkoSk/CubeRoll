using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class MainMenu : MonoBehaviour 
{
	
	#region Variable Declarations
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
        if (!AudioManager.Instance.GetAudioSource(Constants.SOUND_MAINMENU).isPlaying)
        {
            AudioManager.Instance.StopAllMusic();
            AudioManager.Instance.PlaySound(Constants.SOUND_MAINMENU);
        }
	}
	
	private void Update () 
	{
		
	}
	#endregion
	
	
	
	#region Public Functions
	
	#endregion
	
	
	
	#region Private Functions
	
	#endregion
}
