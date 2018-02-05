using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// Author: Melanie Ramsch
/// </summary>
public class MenuBGAction : MonoBehaviour {

	//int spawn_maxX = 130;
	//int spawn_minX = -130;

	//int spawn_maxZ = 83;
	//int spawn_minZ = -65;

	//float spawn_minY = 25f;
	//float spawn_maxY = 100f;

	//float spawnTime_min = 2f;
	//float spawnTime_max = 10f;

	[SerializeField]
	GameObject menuCube;



	private void Update() {
		if(Input.GetButtonDown(Constants.INPUT_FIRE)){
			InstantiateCube(CalculateCubePosition(Input.mousePosition),GetRandomRotation());
		}
	}

	private Vector3 CalculateCubePosition(Vector3 mousePosition){
		//mousePosition.y = getRandomHeight();
		return mousePosition;
	}

	private Vector3 GetMousePosition(){
		return Input.mousePosition;
	}

	private int GetRandomHeight(){
		return Random.Range(25, 100);
	}

	private Quaternion GetRandomRotation() {
		int x = Random.Range(0, 360);
		int y = Random.Range(0, 360);
		int z = Random.Range(0, 360);
		return new Quaternion(x, y, z, 1f);
	}

	private void InstantiateCube(Vector3 position, Quaternion rotation){
		Instantiate(menuCube, position, rotation);

	}




}
