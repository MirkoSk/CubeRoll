using UnityEngine;

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
}
