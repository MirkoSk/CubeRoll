using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadController : MonoBehaviour {

    [SerializeField]
    private float force;

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter(Collider hit) {
        if (hit.tag.Contains("Player")) {
            hit.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
