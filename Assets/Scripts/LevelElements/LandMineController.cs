using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

/// <summary>
/// Knocks the player back on hit.
/// 
/// Author: Mirko Skroch
/// </summary>
[RequireComponent(typeof(Collider))]
public class LandMineController : MonoBehaviour {

    #region Variable Declarations
    [Header("Explosion Stats")]
    public float explosionForce = 3000;
    public float explosionRadius = 5;
    public float movementBlockTimeOnHit = 10;

    [Header("Camera Shake On Hit")]
    public float magnitude = 2f;
    public float roughness = 10f;
    public float fadeOutTime = 3f;

    ParticleSystem ps;
    CameraShaker cameraShakerPlayer1;
    CameraShaker cameraShakerPlayer2;
    #endregion



    #region Unity Event Functions
    void Start() {
        ps = transform.parent.Find("ParticleSystem").GetComponent<ParticleSystem>();

        CameraScript[] cameraRigs = GameObject.FindObjectsOfType<CameraScript>();
        foreach (CameraScript rig in cameraRigs)
        {
            if (rig.playerToFollow == 1) cameraShakerPlayer1 = rig.transform.GetChild(0).GetComponent<CameraShaker>();
            else if (rig.playerToFollow == 2) cameraShakerPlayer2 = rig.transform.GetChild(0).GetComponent<CameraShaker>();
        }
    }

    void OnCollisionEnter(Collision hit) {
        if (hit.transform.tag.Contains(Constants.TAG_PLAYER)) {
            // Aplly damage to the player. If he survives: Also apply the explosion force and block his movement
            if (hit.transform.GetComponent<CubeDamage>().TakeDamage())
            {
                hit.transform.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, this.transform.position + Vector3.up * 1f, explosionRadius);
                hit.transform.GetComponent<CubeController>().BlockMovement(movementBlockTimeOnHit);
            }

            // Trigger effects and sounds
            if (hit.transform.GetComponent<CubeController>().PlayerNumber == 1) cameraShakerPlayer1.ShakeOnce(magnitude, roughness, 0, fadeOutTime);
            else if (hit.transform.GetComponent<CubeController>().PlayerNumber == 2) cameraShakerPlayer2.ShakeOnce(magnitude, roughness, 0, fadeOutTime);
            ps.Play();
            AudioManager.Instance.PlaySound(Constants.SOUND_MINE_PLOP);

            // Update score
            ScoreCounter.Instance.MineDetection(hit.transform.GetComponent<CubeController>().PlayerNumber);
        }
    }
    #endregion
}
