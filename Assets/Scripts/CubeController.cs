using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// Author: Melanie Ramsch, Mirko Skroch
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour {

    #region Variable Declarations
    // Public Variables
    [Header("Movement")]
    public float moveSpeed = 10;
    public float maxSpeed = 20;
    [Tooltip("Dampening of the cube movement. 0 = no input possible, 1 = normal move input")]
    public float moveDampening = 1;
    public AnimationCurve moveDampeningFadeIn = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Other")]
    [Range(1, 2)]
    public int playerNumber = 1;
    [Tooltip("The AnimationCurve used for resetting the cube's position on respawn")]
    public AnimationCurve respawnCurve;

    

    // Private Variables
    // variables for speedy achievement
    [HideInInspector] public float speedDuration;
    [HideInInspector] public bool speedyStarted = false;

    // respawn
    Vector3 startPosition;
    Quaternion startRotation;
    bool respawning;
    public bool Respawning { get { return respawning; } }
    ParticleSystem deathParticleSystem;
    ParticleSystem respawnParticleSystem;
    Renderer cubeRenderer;

    Rigidbody rb;
    #endregion



    #region Unity Event Functions
    private void Start () {
        // Get References
        rb = GetComponent<Rigidbody>();
        cubeRenderer = GetComponent<Renderer>();
        deathParticleSystem = transform.Find("DeathParticleSystem").GetComponent<ParticleSystem>();
        respawnParticleSystem = transform.Find("RespawnParticleSystem").GetComponent<ParticleSystem>();

        // Save the startPosition and Rotation for later use
        startPosition = rb.transform.position;
        startRotation = rb.transform.rotation;

        Spawn();
	}

	private void Update () {
        if (!respawning) MoveCube();

        // Check the player speed and trigger Speedy Gonzalez event, if player is fast enough
        if (!speedyStarted && rb.velocity.x + rb.velocity.z >= ScoreCounter.Instance.SpeedLimit) {
            speedDuration += Time.deltaTime;
            if (speedDuration >= ScoreCounter.Instance.SpeedDuration) {
                ScoreCounter.Instance.Speedy(rb);
                speedyStarted = true;
            }
        }

        if( rb.position.y <= -7f ) Respawn();
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
    private void MoveCube () {
        if (Input.GetAxis(Constants.INPUT_HORIZONTAL + playerNumber) != 0 ) {
            rb.AddForce(Vector3.right * Input.GetAxis(Constants.INPUT_HORIZONTAL + playerNumber) * moveSpeed * moveDampening * 60 * Time.deltaTime);
        }
        if (Input.GetAxis(Constants.INPUT_VERTICAL + playerNumber) != 0 ) {
            rb.AddForce(Vector3.forward * Input.GetAxis(Constants.INPUT_VERTICAL + playerNumber) * moveSpeed * moveDampening * 60 * Time.deltaTime);
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void Respawn() {
        // Make sure we respawn just once
        if (respawning == true) return;
        respawning = true;

        // Stop all motion of the cube
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Hide the cube and add juicyness
        cubeRenderer.enabled = false;
        deathParticleSystem.Play();
        AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_DEATH);

        StartCoroutine(Delay(deathParticleSystem.main.duration * 1.5f, () =>
        {
            // Inform the ScoreCounter to reset the score and spawn a new level
            ScoreCounter.Instance.RespawnTriggered();
            LevelGenerator.Instance.NewLevel();
            
            // Cube magically reappears abvoe it's startPosition
            transform.position = startPosition + Vector3.up * 5f;
            transform.rotation = startRotation;
            cubeRenderer.enabled = true;

            AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_LEVITATE);
            // Softly tween it into it's startPosition
            LeanTween.move(gameObject, startPosition, 2f).setEase(respawnCurve).setOnComplete(() =>
            {
                // Add juicyness
                respawnParticleSystem.Play();
                AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_SPAWN);

                // Reset our speedDuration for Speedy Gonzalez bonus
                speedDuration = 0;

                rb.useGravity = true;
                respawning = false;
            });
        }));
    }

    private void Spawn() {
        // Make sure we respawn just once
        if (respawning == true) return;
        respawning = true;

        // Cube magically appears abvoe it's startPosition
        rb.useGravity = false;
        transform.position = startPosition + Vector3.up * 5f;
        cubeRenderer.enabled = true;

        AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_LEVITATE);
        // Softly tween it into it's startPosition
        LeanTween.move(gameObject, startPosition, 2f).setEase(respawnCurve).setOnComplete(() =>
        {
            // Add juicyness
            respawnParticleSystem.Play();
            AudioManager.Instance.PlaySound(Constants.SOUND_CUBE_SPAWN);

            // Reset our speedDuration for Speedy Gonzalez bonus
            speedDuration = 0;

            rb.useGravity = true;
            respawning = false;
        });
    }
    #endregion



    #region Coroutines
    IEnumerator ResetMoveDampeningCoroutine(float fadeSpeed) {
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
