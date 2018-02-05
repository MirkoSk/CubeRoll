using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles all scoring and informs the UI components accordingly.
/// 
/// Author: Melanie Ramsch, Mirko Skroch
/// </summary>
public class ScoreCounter : MonoBehaviour {

    #region Class Definitions
    [System.Serializable]
    public class ObjectReferences
    {
        public ScoreUpdater scoreUpdater;
        public TextMeshProUGUI infoText;
        public TextMeshProUGUI infoText2;
        public HighscoreUpdater highscoreUpdater;
        public DistanceUpdater distanceUpdater;
        public CubeScript cubeScript;
    }
    #endregion



    #region Variable Declarations
    // Static singleton property
    public static ScoreCounter Instance { get; private set; }

    // Public Variables
    [Header("Achievement Points")]
    [SerializeField] int tileCompletion = 100;
    [SerializeField] int mineDetectionDog = 200;
    [Tooltip("Points per frame")]
    [SerializeField] int speedyGonzalez = 1;

    [Header("Speedy Gonzalez")]
    [SerializeField] float speedLimit = 12f;
    [Tooltip("Time the speed limit needs to be surpassed, to get the achievement.")]
    [SerializeField] float speedDuration = 3f;
    
    [Space]
    [SerializeField] ObjectReferences objectReferences;

    // Public Properties
    public int Score { get { return score; } }
    public int Distance { get { return distance; } }
    public int Highscore { get { return highscore; } }
    public float SpeedDuration { get { return speedDuration; } }
    public float SpeedLimit { get { return speedLimit; } }

    // Private Variables
    int score;
    int distance;
    int highscore;

    GameObject cube;
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
        cube = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER);
    }

    void Update() {

        UpdateDistance();

        // Update the score and distance UI
        objectReferences.distanceUpdater.UpdateText();
        objectReferences.scoreUpdater.UpdateText();
    }
    #endregion



    #region Public Functions
    // Functions for everything that gives points
    public void TileCompleted() {
        AddPoints(tileCompletion);
        objectReferences.infoText.text = "Tile Completed !";
        objectReferences.infoText2.text = tileCompletion.ToString();
        StartCoroutine(HideTextFast());
    }

    public void MineDetection() {
        AddPoints(mineDetectionDog);
        objectReferences.infoText.text = "Mine Detection Dog !";
        objectReferences.infoText2.text = mineDetectionDog.ToString();
        StartCoroutine(HideText());
    }

    public void Speedy(Rigidbody rb) {
        objectReferences.infoText.text = "Speedy Gonzalez !";
        StartCoroutine(SpeedyCoroutine(rb));
    }

    public void RespawnTriggered() {
        AddPoints(distance);
        if (score > highscore)
        {
            highscore = score;
            objectReferences.highscoreUpdater.UpdateText();
            ResetScore();
        }
    }
    #endregion



    #region Private Functions
    void AddPoints(int points)
    {
        this.score += points;
        objectReferences.scoreUpdater.UpdateText();
    }

    void ResetScore()
    {
        score = 0;
        objectReferences.scoreUpdater.UpdateText();
    }

    void UpdateDistance()
    {
        if (cube.transform.position.z > 0)
        {
            distance = (int)cube.transform.position.z;
        }
        else
        {
            distance = 0;
        }
    }
    #endregion



    #region Coroutines
    IEnumerator HideText() {
        yield return new WaitForSecondsRealtime(2.5f);
        objectReferences.infoText.text = "";
        objectReferences.infoText2.text = "";
    }

    IEnumerator HideTextFast()
    {
        yield return new WaitForSecondsRealtime(1f);
        objectReferences.infoText.text = "";
        objectReferences.infoText2.text = "";
    }

    IEnumerator SpeedyCoroutine(Rigidbody rb) {
        int multiplier = 0;
        while (rb.velocity.x + rb.velocity.z >= speedLimit) {
            AddPoints(speedyGonzalez);
            multiplier++;
            objectReferences.infoText.text = "Speedy Gonzalez x" + multiplier + " !";
            objectReferences.infoText2.text = (multiplier * speedyGonzalez).ToString();
            yield return new WaitForSecondsRealtime(0.1f);
        }
        StartCoroutine(HideText());
        objectReferences.cubeScript.speedDuration = 0;
        objectReferences.cubeScript.speedyStarted = false;
    }
    #endregion
}
