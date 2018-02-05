using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// Author: Melanie Ramsch
/// </summary>
public class MenuCubeController : MonoBehaviour {


	private void Update() {
		this.gameObject.GetComponent<Rigidbody>().AddForce(GetRandomDirection()*GetRandomSpeed());
		//this.gameObject.GetComponent<Rigidbody>().velocity = Mathf.Clamp(this.gameObject.GetComponent<Rigidbody>().velocity.magnitude, 2f, 5f);
	}

	private Vector3 GetRandomDirection() {
		int x = Random.Range(0, 360);
		int y = Random.Range(0, 360);
		int z = Random.Range(0, 360);
		return new Vector3(x, y, z);
	}

	private float GetRandomSpeed(){
		//return Random.Range(0.1f, 2f);
		return 1f;
	}

}
