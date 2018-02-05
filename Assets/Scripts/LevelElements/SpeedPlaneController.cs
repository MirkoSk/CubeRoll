using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Applies a forward force to the player on OnTriggerStay.
/// 
/// Author: Mirko Skroch
/// </summary>
[RequireComponent(typeof(Collider))]
public class SpeedPlaneController : MonoBehaviour {

    public float force = 1;



    void OnTriggerStay(Collider hit) {
        if (hit.tag.Contains(Constants.TAG_PLAYER)) {
            hit.GetComponent<Rigidbody>().AddForce(this.transform.forward * force);
        }
    }
}
