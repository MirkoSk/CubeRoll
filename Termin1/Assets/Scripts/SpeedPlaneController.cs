using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlaneController : MonoBehaviour {

    public float force = 1;

    void OnTriggerStay(Collider hit) {
        if (hit.tag.Contains("Player")) {
            hit.GetComponent<Rigidbody>().AddForce(this.transform.forward * force);
        }
    }
}
