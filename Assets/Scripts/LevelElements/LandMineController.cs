using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

/// <summary>
/// Knocks the player back on hit.
/// 
/// Author: Mirko Skroch
/// </summary>
public class LandMineController : MonoBehaviour {

    #region Variable Declarations
    [Header("Explosion Stats")]
    public float explosionForce = 1000;
    public float explosionRadius = 5;
    public float movementBlockTimeOnHit = 3;

    [Header("Camera Shake On Hit")]
    public float magnitude = 2f;
    public float roughness = 10f;
    public float fadeOutTime = 5f;

    private ParticleSystem ps;
    #endregion



    #region Unity Event Functions
    void Start() {
        ps = transform.parent.Find("ParticleSystem").GetComponent<ParticleSystem>();
    }

    void OnCollisionEnter(Collision hit) {
        if (hit.transform.tag.Contains(Constants.TAG_PLAYER)) {
            // Aplly force to player and block his movement
            hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, this.transform.position + Vector3.up * 1f, explosionRadius);
            hit.gameObject.GetComponent<CubeScript>().BlockMovement(movementBlockTimeOnHit);

            // Trigger effects and sounds
            CameraShaker.Instance.ShakeOnce(magnitude, roughness, 0, fadeOutTime);
            ps.Play();
            AudioManager.Instance.PlaySound(Constants.SOUND_MINE_PLOP);

            // Update score
            ScoreCounter.Instance.MineDetection();
        }
    }
    #endregion
}
