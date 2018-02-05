using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Applies an upward force to the player over the specified duration.
/// 
/// Author: Mirko Skroch
/// </summary>
public class JumpPadController : MonoBehaviour {

    #region Variable Declarations
    [Space]
    public float forceAmount = 15f;
    [Tooltip("The duration the force is applied to the player in seconds.")]
    public float duration = 1f;
    #endregion



    #region Unity Event Functions
    void OnTriggerEnter(Collider hit) {
        if (hit.tag.Contains(Constants.TAG_PLAYER)) {
            StartCoroutine(AddJumpForce(hit.GetComponent<Rigidbody>()));
        }
    }
    #endregion



    #region Coroutines
    IEnumerator AddJumpForce(Rigidbody rb) {
        for (float i = 0; i < duration; i += Time.deltaTime) {
            rb.AddForce(Vector3.up * forceAmount);
            yield return null;
        }
    }
    #endregion
}
