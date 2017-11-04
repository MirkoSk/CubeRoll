using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    [Header("Achievement Points")]
    public int tileCompletion = 100;
    public int mineDetectionDog = 200;
    [Tooltip("Points per frame")]
    public int speedyGonzalez = 1;

    [Header("Speedy Gonzalez")]
    public float speedLimit = 12f;
    [Tooltip("Time the speed limit needs to be surpassed, to get the achievement.")]
    public float speedDuration = 3f;

    [Header("Object References")]
    public Text scoreText;
    public Text infoText;
    public Text infoText2;
    public Text highscoreText;
    public CubeScript cubeScript;

    [HideInInspector]
    public int score;
    private int highscore;

    // Static singleton property
    public static ScoreCounter Instance { get; private set; }



    void Awake() {
        // Save a reference to the AudioHandler component as our singleton instance
        Instance = this;
    }

    void Update() {
        scoreText.text = score.ToString();
    }


    
    // Functions for everything that gives points

    public void tileCompleted() {
        score += tileCompletion;
        infoText.text = "Tile Completed !";
        infoText2.text = tileCompletion.ToString();
    }

    public void mineDetection() {
        score += mineDetectionDog;
        infoText.text = "Mine Detection Dog !";
        infoText2.text = mineDetectionDog.ToString();
        StartCoroutine(hideText());
    }

    public void speedy(Rigidbody rb) {
        infoText.text = "Speedy Gonzalez !";
        StartCoroutine(speedyCoroutine(rb));
    }

    public void respawnTriggered() {
        if (score > highscore)
            highscore = score;
            highscoreText.text = highscore.ToString();
        score = 0;
    }



    IEnumerator hideText() {
        yield return new WaitForSecondsRealtime(2.5f);
        infoText.text = "";
        infoText2.text = "";
    }

    IEnumerator speedyCoroutine(Rigidbody rb) {
        int multiplier = 0;
        while (rb.velocity.x + rb.velocity.z >= speedLimit) {
            score += speedyGonzalez;
            multiplier++;
            infoText.text = "Speedy Gonzalez x" + multiplier + " !";
            infoText2.text = (multiplier * speedyGonzalez).ToString();
            yield return new WaitForSecondsRealtime(0.1f);
        }
        StartCoroutine(hideText());
        cubeScript.speedDuration = 0;
        cubeScript.speedyStarted = false;
    }
}
