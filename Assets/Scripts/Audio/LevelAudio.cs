using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for selecting the background track for a level.
/// 
/// Author: Mirko Skroch
/// </summary>
public class LevelAudio : MonoBehaviour {

    #region Variable Declarations
    [Range(1,2)]
    [SerializeField] int backgroundTrack = 1;
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
        {
        if (backgroundTrack == 1) 
        {
            AudioManager.Instance.PlaySound(Constants.SOUND_TRACK01INTRO);
            AudioManager.Instance.PlaySoundScheduled(Constants.SOUND_TRACK01LOOP, AudioSettings.dspTime + AudioManager.Instance.GetAudioSource(Constants.SOUND_TRACK01INTRO).clip.length - 0.08);
        }
        else if (backgroundTrack == 2) {
            AudioManager.Instance.PlaySound(Constants.SOUND_TRACK02LOOP);
        }
	}
	
	private void Update () 
    {
		
	}
    #endregion
}
