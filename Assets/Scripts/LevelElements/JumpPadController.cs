using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Applies an upward force to the player over the specified duration.
/// 
/// Author: Mirko Skroch
/// </summary>
[RequireComponent(typeof(Collider))]
public class JumpPadController : MonoBehaviour {

    #region Variable Declarations
    [Space]
    public float forceAmount = 15f;
    [Tooltip("The duration the force is applied to the player in seconds.")]
    public float forceDuration = 1f;
    #endregion



    #region Unity Event Functions
    void OnTriggerEnter(Collider hit)
    {
        // Since Triggers don't understand compound colliders: Go up the hierarchy until you hit the gameObject with the rigidbody
        Rigidbody parent = hit.FindComponentInParents<Rigidbody>();

        if (parent.tag.Contains(Constants.TAG_PLAYER)) {
            StartCoroutine(AddJumpForce(parent));
        }
    }
    #endregion



    #region Coroutines
    IEnumerator AddJumpForce(Rigidbody rb) {
        for (float i = 0; i < forceDuration; i += Time.deltaTime) {
            rb.AddForce(transform.up * forceAmount);
            yield return null;
        }
    }
    #endregion
}
