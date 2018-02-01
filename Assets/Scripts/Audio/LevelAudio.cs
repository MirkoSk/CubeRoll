using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class LevelAudio : MonoBehaviour {

    #region Variable Declarations
    [Range(1,2)]
    [SerializeField] int backgroundTrack;
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
        {
        if (backgroundTrack == 1) 
        {
            AudioManager.Instance.PlaySound("Track01Intro");
            AudioManager.Instance.PlaySoundScheduled("Track01Loop", AudioSettings.dspTime + AudioManager.Instance.GetAudioSource("Track01Intro").clip.length - 0.1);
        }
        else if (backgroundTrack == 2) {
            AudioManager.Instance.PlaySound("Track02Loop");
        }
	}
	
	private void Update () 
    {
		
	}
    #endregion



    #region Private Functions

    #endregion
}
