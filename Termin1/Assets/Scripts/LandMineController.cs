using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineController : MonoBehaviour {

    public float explosionForce = 1000;
    public float explosionRadius = 5;
    public float movementBlockTimeOnHit = 3;

    private ParticleSystem ps;

    void Start() {
        ps = GetComponent<ParticleSystem>();
    }

    void OnCollisionEnter(Collision hit) {
        if (hit.transform.tag.Contains("Player")) {
            hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, this.transform.position + Vector3.up * 0.25f, explosionRadius);
            hit.gameObject.GetComponent<CubeScript>().blockMovement(movementBlockTimeOnHit);
            ps.Play();
        }
    }
}
