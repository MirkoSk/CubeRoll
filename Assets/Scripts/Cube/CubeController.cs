using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// 
/// Author: Melanie Ramsch, Mirko Skroch
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour
{
    #region Class Definitions
    [System.Serializable]
    public class ObjectReferences
    {
        public GameObject meshes;
        public ParticleSystem deathParticleSystem;
        public ParticleSystem respawnParticleSystem;
    }
    #endregion



    #region Variable Declarations
    // Visible in Inspector
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float maxSpeed = 20;
    [Tooltip("Dampening of the cube movement. 0 = no input possible, 1 = normal move input")]
    [SerializeField] float moveDampening = 1;
    [SerializeField] AnimationCurve moveDampeningFadeIn = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Other")]
    [Range(1, 2)]
    [SerializeField] int playerNumber = 1;
    [Tooltip("The AnimationCurve used for resetting the cube's position on respawn")]
    [SerializeField] AnimationCurve respawnCurve;

    [Space]
    [SerializeField] ObjectReferences references;

    // Public Properties
    public bool Respawning { get { return respawning; } }
    public int PlayerNumber { get { return playerNumber; } }
    public float SpeedDuration { get; set; }
    public bool SpeedyStarted { get; set; }
    public bool CubeOnTrack { get { return cubeOnTrack; } }
    public Vector3 CurrentTilePosition { get { return currenTilePosition; } set { currenTilePosition = value; } }

    // Private Variables
    // variables for speedy achievement
    float speedDuration;
    bool speedyStarted = false;

    // respawn
    Vector3 startPosition;
    Quaternion startRotation;
    bool respawning;
    Transform otherCube;
    bool cubeOnTrack;
    Vector3 currenTilePosition;

    Rigidbody rb;
    #endregion



    #region Unity Event Functions
    private void Start () {
        // Get References
        rb = GetComponent<Rigidbody>();

        // Save the startPosition and Rotation for later use
        startPosition = rb.transform.position;
        startRotation = rb.transform.rotation;
        currenTilePosition = rb.transform.position;

        // Save a reference to the other cube, if this is a multiplayer game
        GameObject[] players = GameObject.FindGameObjectsWithTag(Constants.TAG_PLAYER);
        foreach (GameObject go in players)
        {
            if (go != gameObject) otherCube = go.transform;
        }

        Spawn();
	}

	private void Update () {
        if (!respawning) MoveCube();

        // Check the player speed and trigger Speedy Gonzalez event, if player is fast enough
        if (!speedyStarted && rb.velocity.x + rb.velocity.z >= ScoreCounter.Instance.SpeedLimit) {
            speedDuration += Time.deltaTime;
            if (speedDuration >= ScoreCounter.Instance.SpeedDuration) {
                ScoreCounter.Instance.Speedy(rb, playerNumber);
                speedyStarted = true;
            }
        }

        cubeOnTrack = Physics.Raycast(transform.position, Vector3.down, 20f);

        if ( rb.position.y <= -7f ) Respawn();
		SavePlayerScoreToDataClass();
	}
    #endregion



    #region Public Functions
    public void BlockMovement(float seconds)
    {
        moveDampening = 0;
        StartCoroutine(ResetMoveDampeningCoroutine(seconds));
    }
    #endregion



    #region Private Functions
    private void MoveCube ()
    {
        if (Input.GetAxis(Constants.INPUT_HORIZONTAL + playerNumber) != 0)
        {
            rb.AddForce(Vector3.right * Input.GetAxis(Constants.INPUT_HORIZONTAL + playerNumber) * moveSpeed * moveDampening * 60 * Time.deltaTime);
        }
        if (Input.GetAxis(Constants.INPUT_VERTICAL + playerNumber) != 0)
        {
            rb.AddForce(Vector3.forward * Input.GetAxis(Constants.INPUT_VERTICAL + playerNumber) * moveSpeed * moveDampening * 60 * Time.deltaTime);
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void Respawn()
    {
        // Make sure to respawn just once
        if (respawning == true) return;
        respawning = true;

		StopCubeMovement();
		KillCube();
		AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_DEATH);

		StartCoroutine(Delay(references.deathParticleSystem.main.duration * 1.5f, () =>
        {
			NotifyScoreCounter();

			if (!Data.singlePlayerGame)
            {
                if (otherCube.GetComponent<CubeController>().CubeOnTrack)
                {
                    MultiplayerRespawnAtOtherPlayersPosition();
                }
                else
                {
                    MultiplayerRespawnAtCurrentTile();
                }
			}
            else
            {
				SinglePlayerGameOver();
			}

            AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_LEVITATE);
			TweenCubeToGround();           
        }));
    }

	private void SavePlayerScoreToDataClass(){
		Data.singlePlayerScore = ScoreCounter.Instance.Score1;
		Data.player1 = ScoreCounter.Instance.Score1;
		Data.player2 = ScoreCounter.Instance.Score2;
	}
	private void StopCubeMovement() {
		rb.useGravity = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}
	private void KillCube(){
		references.meshes.SetActive(false);
		references.deathParticleSystem.Play();
	}
	private void NotifyScoreCounter(){
		ScoreCounter.Instance.RespawnTriggered(playerNumber);
	}
	private void MultiplayerRespawnAtOtherPlayersPosition(){
		transform.position = new Vector3(otherCube.position.x, 6f, otherCube.position.z);
		transform.rotation = startRotation;
		references.meshes.SetActive(true);
	}
    private void MultiplayerRespawnAtCurrentTile()
    {
        transform.position = new Vector3(currenTilePosition.x, 6f, currenTilePosition.z);
        transform.rotation = startRotation;
        references.meshes.SetActive(true);
    }
    private void SinglePlayerGameOver(){
		SceneManager.LoadScene(Constants.HIGHSCORE_SCENE, LoadSceneMode.Single);
	}



	private void TweenCubeToGround(){
		// Softly tween the cube onto the ground
		LeanTween.move(gameObject, new Vector3(transform.position.x, 1f, transform.position.z), 2f).setEase(respawnCurve).setOnComplete(() =>
		{
			// Add juicyness
			references.respawnParticleSystem.Play();
			AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_SPAWN);

			// Reset our speedDuration for Speedy Gonzalez bonus
			speedDuration = 0;

			rb.useGravity = true;
			respawning = false;
		});
	}

    private void Spawn()
    {
        // Make sure we respawn just once
        if (respawning == true) return;
        respawning = true;

        // Cube magically appears abvoe it's startPosition
        rb.useGravity = false;
        transform.position = startPosition + Vector3.up * 5f;
        references.meshes.SetActive(true);

        AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_LEVITATE);
        // Softly tween it into it's startPosition
        LeanTween.move(gameObject, startPosition, 2f).setEase(respawnCurve).setOnComplete(() =>
        {
            // Add juicyness
            references.respawnParticleSystem.Play();
            AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_SPAWN);

            // Reset our speedDuration for Speedy Gonzalez bonus
            speedDuration = 0;

            rb.useGravity = true;
            respawning = false;
        });
    }
    #endregion



    #region Coroutines
    IEnumerator ResetMoveDampeningCoroutine(float fadeSpeed)
    {
        for (float i = 0; i < 1; i += 0.0125f * fadeSpeed * 60 * Time.deltaTime) {
            moveDampening = moveDampeningFadeIn.Evaluate(i);
            yield return null;
        }
        moveDampening = 1;
    }

    IEnumerator Delay(float time, System.Action onComplete)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            yield return null;
        }
        onComplete.Invoke();
    }
    #endregion
}
