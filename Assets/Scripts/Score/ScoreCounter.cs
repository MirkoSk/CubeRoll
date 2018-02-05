﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public InfoTextUpdater infoTextUpdater;
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
    public int SpeedyGonzalez { get { return speedyGonzalez; } }

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
        objectReferences.infoTextUpdater.UpdateText("Tile Completed !", tileCompletion.ToString(), 1f);
    }

    public void MineDetection() {
        AddPoints(mineDetectionDog);
        objectReferences.infoTextUpdater.UpdateText("Mine Detection Dog !", mineDetectionDog.ToString(), 2.5f);
    }

    public void Speedy(Rigidbody rb) {
        objectReferences.infoTextUpdater.UpdateText("Speedy Gonzalez !", "", 2.5f);
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

    public void AddPoints(int points)
    {
        this.score += points;
        objectReferences.scoreUpdater.UpdateText();
    }
    #endregion



    #region Private Functions
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
    IEnumerator SpeedyCoroutine(Rigidbody rb)
    {
        int multiplier = 0;
        while (rb.velocity.x + rb.velocity.z >= speedLimit)
        {
            AddPoints(speedyGonzalez);
            multiplier++;
            objectReferences.infoTextUpdater.UpdateText("Speedy Gonzalez x" + multiplier + " !", (multiplier * speedyGonzalez).ToString(), 2.5f);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        objectReferences.cubeScript.speedDuration = 0;
        objectReferences.cubeScript.speedyStarted = false;
    }
    #endregion
}
