using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all scoring and informs the UI components accordingly.
/// 
/// Author: Mirko Skroch
/// </summary>
public class ScoreCounter : MonoBehaviour
{
    #region Class Definitions
    [System.Serializable]
    public class PlayerState
    {
        public int score;
        public int distance;
        public int highscore;
        public ObjectReferences references;

        [System.Serializable]
        public class ObjectReferences
        {
            public ScoreUpdater scoreUpdater;
            public InfoTextUpdater infoTextUpdater;
            public HighscoreUpdater highscoreUpdater;
            public DistanceUpdater distanceUpdater;
            public CubeController cube;
        }
    }
    #endregion



    #region Variable Declarations
    // Static singleton property
    public static ScoreCounter Instance { get; private set; }

    // Visible in Inspector
    [Header("Achievement Points")]
    [SerializeField] protected int tileCompletion = 50;
    [SerializeField] protected int mineDetectionDog = 100;
    [Tooltip("Points per frame")]
    [SerializeField] protected int speedyGonzalez = 5;

    [Header("Speedy Gonzalez")]
    [SerializeField] protected float speedLimit = 10f;
    [Tooltip("Time the speed limit needs to be surpassed, to get the achievement.")]
    [SerializeField] protected float speedDuration = 1.5f;

    [Space]
    [SerializeField] PlayerState player1;
    [SerializeField] PlayerState player2;

    // Public Properties
    public int Score1 { get { return player1.score; } }
    public int Score2 { get { return player2.score; } }
    public int Distance1 { get { return player1.distance; } }
    public int Distance2 { get { return player2.distance; } }
    public int Highscore1 { get { return player1.highscore; } }
    public int Highscore2 { get { return player2.highscore; } }
    public float SpeedDuration { get { return speedDuration; } }
    public float SpeedLimit { get { return speedLimit; } }
    public int SpeedyGonzalez { get { return speedyGonzalez; } }
    #endregion



    #region Unity Event Functions
    private void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
        {
            //if not, set instance to this
            Instance = this;
        }

        //If instance already exists and it's not this:
        else if (Instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of an AudioManager.
            Destroy(this);
        }
    }

    private void Start()
    {
		if(player2.references.cube == null) {
			Data.singlePlayerGame = true;
		} else {
			Data.singlePlayerGame = false;
		}

		player1.highscore = getHighestScore();
    }

    private void Update()
    {
        UpdateDistances();

        // Update the score and distance UIs
        player1.references.distanceUpdater.UpdateText();
        player1.references.scoreUpdater.UpdateText();

        if (!Data.singlePlayerGame)
        {
            player2.references.distanceUpdater.UpdateText();
            player2.references.scoreUpdater.UpdateText();
        }
    }
    #endregion



    #region Public Functions
    // Functions for everything that awards points
    public void TileCompleted(int playerNumber)
    {
        AddPoints(tileCompletion, playerNumber);

        if (playerNumber == 1) player1.references.infoTextUpdater.UpdateText("Tile Completed !", tileCompletion.ToString(), 1f);
        else if (playerNumber == 2) player2.references.infoTextUpdater.UpdateText("Tile Completed !", tileCompletion.ToString(), 1f);

        AudioManager.Instance.PlaySoundOneShot(Constants.SOUND_SCORE_UP);
    }

    public void MineDetection(int playerNumber)
    {
        AddPoints(mineDetectionDog, playerNumber);

        if (playerNumber == 1) player1.references.infoTextUpdater.UpdateText("Mine Detection Dog !", mineDetectionDog.ToString(), 2.5f);
        else if (playerNumber == 2) player2.references.infoTextUpdater.UpdateText("Mine Detection Dog !", mineDetectionDog.ToString(), 2.5f);

        AudioManager.Instance.PlaySoundOneShot(Constants.SOUND_SCORE_UP);
    }

    public void Speedy(Rigidbody rb, int playerNumber)
    {
        if (playerNumber == 1) player1.references.infoTextUpdater.UpdateText("Speedy Gonzalez !", "", 2.5f);
        else if (playerNumber == 2) player2.references.infoTextUpdater.UpdateText("Speedy Gonzalez !", "", 2.5f);

        StartCoroutine(SpeedyCoroutine(rb, playerNumber));
        AudioManager.Instance.PlaySoundOneShot(Constants.SOUND_SCORE_UP);
    }

    public void RespawnTriggered(int playerNumber)
    {
        if (Data.singlePlayerGame)
        {
            AddPoints(player1.distance, playerNumber);

            if (player1.score > player1.highscore)
            {
                player1.highscore = player1.score;
                player1.references.highscoreUpdater.UpdateText();
                ResetScore(playerNumber);
            }
        }
    }
    #endregion



    #region Private Functions
    void AddPoints(int points, int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1.score += points;
            player1.references.scoreUpdater.UpdateText();
        }
        else if (playerNumber == 2)
        {
            player2.score += points;
            player2.references.scoreUpdater.UpdateText();
        }
    }

    void ResetScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1.score = 0;
            player1.references.scoreUpdater.UpdateText();
        }
        else if (playerNumber == 2)
        {
            player2.score = 0;
            player2.references.scoreUpdater.UpdateText();
        }
    }

    void UpdateDistances()
    {
        // Update Distance of player 1
        if (player1.references.cube.transform.position.z > 0)
        {
            player1.distance = (int)player1.references.cube.transform.position.z;
        }
        else
        {
            player1.distance = 0;
        }

        // Update Distance of player 2
        if (!Data.singlePlayerGame && player2.references.cube.transform.position.z > 0)
        {
            player2.distance = (int)player2.references.cube.transform.position.z;
        }
        else
        {
            player2.distance = 0;
        }
    }

	private int getHighestScore() {
		if(PlayerPrefs.HasKey("name0")) return PlayerPrefs.GetInt("points0");
		else return 0;
	}
	#endregion



	#region Coroutines
	IEnumerator SpeedyCoroutine(Rigidbody rb, int playerNumber)
    {
        int multiplier = 0;
        while (rb.velocity.x + rb.velocity.z >= speedLimit)
        {
            AddPoints(speedyGonzalez, playerNumber);
            multiplier++;
            if (playerNumber == 1) player1.references.infoTextUpdater.UpdateText("Speedy Gonzalez x" + multiplier + " !", (multiplier * speedyGonzalez).ToString(), 2.5f);
            else if (playerNumber == 2) player2.references.infoTextUpdater.UpdateText("Speedy Gonzalez x" + multiplier + " !", (multiplier * speedyGonzalez).ToString(), 2.5f);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (playerNumber == 1)
        {
            player1.references.cube.SpeedDuration = 0;
            player1.references.cube.SpeedyStarted = false;
        }
        else if (playerNumber == 2)
        {
            player2.references.cube.SpeedDuration = 0;
            player2.references.cube.SpeedyStarted = false;
        }
    }
    #endregion
}
