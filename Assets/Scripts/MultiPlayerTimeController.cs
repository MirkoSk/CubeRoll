using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Counts down the time for our Multiplayer and loads the score scene if it reaches 0.
/// 
/// Author: Melanie Ramsch, Mirko Skroch
/// </summary>
public class MultiPlayerTimeController : MonoBehaviour
{
    #region Variable Declarations
    // Configuration
    [SerializeField] float duration = 60f;

    // State
	float restTime = 0;

    // Public Properties
    public float RestTime { get { return restTime; } }
    public float Duration { get { return duration; } }
    #endregion



    #region Unity Event Functions
    private void Start()
    {
        restTime = duration;
	}

	private void Update()
    {
        restTime -= Time.deltaTime;

		if (restTime <= 0f)
        {
			SceneManager.LoadScene(Constants.MULTIPLAYER_SCORE_SCENE);
		}
	}
    #endregion
}
