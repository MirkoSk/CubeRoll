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
            StartCoroutine(addJumpForce(hit.GetComponent<Rigidbody>()));
            //hit.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }

    IEnumerator addJumpForce(Rigidbody rb) {
        for (int i = 0; i < 60; i++) {
            rb.AddForce(Vector3.up * force);
            yield return null;
        }
    }
}
