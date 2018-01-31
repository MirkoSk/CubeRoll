using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGAction : MonoBehaviour {

	int spawn_maxX = 130;
	int spawn_minX = -130;

	int spawn_maxZ = 83;
	int spawn_minZ = -65;

	float spawn_minY = 25f;
	float spawn_maxY = 100f;

	float spawnTime_min = 2f;
	float spawnTime_max = 10f;

	[SerializeField]
	GameObject menuCube;



	private void Update() {
		if(Input.GetButtonDown("Fire1")){
			instantiateCube(calculateCubePosition(Input.mousePosition),getRandomRotation());
		}
	}

	private Vector3 calculateCubePosition(Vector3 mousePosition){
		//mousePosition.y = getRandomHeight();
		return mousePosition;
	}

	private Vector3 getMousePosition(){
		return Input.mousePosition;
	}

	private int getRandomHeight(){
		return Random.Range(25, 100);
	}

	private Quaternion getRandomRotation() {
		int x = Random.Range(0, 360);
		int y = Random.Range(0, 360);
		int z = Random.Range(0, 360);
		return new Quaternion(x, y, z, 1f);
	}

	private void instantiateCube(Vector3 position, Quaternion rotation){
		Instantiate(menuCube, position, rotation);

	}




}
