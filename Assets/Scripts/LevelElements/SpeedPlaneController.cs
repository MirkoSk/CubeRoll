using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Applies a forward force to the player on OnTriggerStay.
/// 
/// Author: Mirko Skroch
/// </summary>
[RequireComponent(typeof(Collider))]
public class SpeedPlaneController : MonoBehaviour
{

    public float force = 50f;



    void OnTriggerStay(Collider hit)
    {
        // Since Triggers don't understand compound colliders: Go up the hierarchy until you hit the gameObject with the rigidbody
        Rigidbody parent = hit.FindComponentInParents<Rigidbody>();

        if (parent.tag.Contains(Constants.TAG_PLAYER)) {
            parent.AddForce(this.transform.forward * force);
        }
    }
}
