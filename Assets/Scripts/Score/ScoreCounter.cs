using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles all scoring and updates the UI accordingly.
/// 
/// Author: Melanie Ramsch, Mirko Skroch
/// </summary>
public class ScoreCounter : MonoBehaviour {

    #region Variable Declarations
    [Header("Achievement Points")]
    public int tileCompletion = 100;
    public int mineDetectionDog = 200;
    [Tooltip("Points per frame")]
    public int speedyGonzalez = 1;

    [Header("Speedy Gonzalez")]
    public float speedLimit = 12f;
    [Tooltip("Time the speed limit needs to be surpassed, to get the achievement.")]
    public float speedDuration = 3f;
    
    [System.Serializable]
    public class ObjectReferences
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI infoText;
        public TextMeshProUGUI infoText2;
        public TextMeshProUGUI highscoreText;
        public CubeScript cubeScript;
        public DistanceCounter distanceCounter;
    }
    public ObjectReferences objectReferences;

    [HideInInspector]
    public int score;
    private int highscore;

    // Static singleton property
    public static ScoreCounter Instance { get; private set; }
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

    void Update() {
        objectReferences.scoreText.text = (objectReferences.distanceCounter.distance + score).ToString();
    }
    #endregion



    #region Public Functions
    // Functions for everything that gives points
    public void TileCompleted() {
        score += tileCompletion;
        objectReferences.infoText.text = "Tile Completed !";
        objectReferences.infoText2.text = tileCompletion.ToString();
        StartCoroutine(HideTextFast());
    }

    public void MineDetection() {
        score += mineDetectionDog;
        objectReferences.infoText.text = "Mine Detection Dog !";
        objectReferences.infoText2.text = mineDetectionDog.ToString();
        StartCoroutine(HideText());
    }

    public void Speedy(Rigidbody rb) {
        objectReferences.infoText.text = "Speedy Gonzalez !";
        StartCoroutine(SpeedyCoroutine(rb));
    }

    public void RespawnTriggered() {
        score += objectReferences.distanceCounter.distance;
        if (score > highscore)
        {
            highscore = score;
            objectReferences.highscoreText.text = highscore.ToString();
            score = 0;
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
            score += speedyGonzalez;
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
