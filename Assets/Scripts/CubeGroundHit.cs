using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGroundHit : MonoBehaviour {

    public GameObject cubeGroundHitPSPrefab;

    private void Start() {
    }

    private void OnCollisionEnter(Collision collision) {
        for (int i = 0; i < collision.contacts.Length; i++) {
            Instantiate(cubeGroundHitPSPrefab, collision.contacts[i].point + Vector3.up * 0.3f, Quaternion.identity);
            AudioManager.Instance.PlaySound("CubeHit");
        }
    }
}
