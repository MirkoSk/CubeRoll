using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGroundHit : MonoBehaviour {

    public GameObject cubeGroundHitPSPrefab;

    private void Start() {
    }

    private void OnCollisionEnter(Collision collision) {
        GameObject cubeGroundHitPS = Instantiate(cubeGroundHitPSPrefab, collision.contacts[0].point + Vector3.up * 0.3f, Quaternion.identity);
    }
}
