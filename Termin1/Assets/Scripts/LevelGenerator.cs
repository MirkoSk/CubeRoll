using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour {

    //Tiles
    [Header("Tileset 1")]
    public GameObject tile1_1;
    public GameObject tile1_2;
    public GameObject tile1_3;
    [Header("Tileset 2")]
    public GameObject tile2_1;
    public GameObject tile2_2;
    public GameObject tile2_3;

    //Liste mit Tiles
    List<GameObject> tilelist ;

    //Cycle
    private bool even = false;


    // Use this for initialization
    void Start () {
        generateLevel();
        drawLevel();
        tilelist = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void addNewTile (Vector3 position, Quaternion rotation) {
        GameObject currentTile = null;
        int random = Random.Range(1, 3);
        if( even ) {
            switch( random ) {
                case 1:
                currentTile = tile1_1;
                break;
                case 2:
                currentTile = tile1_2;
                break;
                case 3:
                currentTile = tile1_3;
                break;
            }
            Instantiate(currentTile, position, rotation);
            even = false;
        }
        else {
            switch( random ) {
                case 1:
                currentTile = tile2_1;
                break;
                case 2:
                currentTile = tile2_2;
                break;
                case 3:
                currentTile = tile2_3;
                break;
            }
            Instantiate(currentTile, position, rotation);
            even = false;
        }
    }
    private void generateLevel() { 
        GameObject currentTile = null;
        for (int i=1; i<5; i++){
            int random = Random.Range(1, 3);
            if(i%2==0) {
                even = false;
                switch( random ) {
                    case 1: currentTile = tile1_1;
                    break;
                    case 2: currentTile = tile1_2;
                    break;
                    case 3: currentTile = tile1_3;
                    break;
                }
                tilelist.Add(currentTile);
            }
            else {
                even = true;
                switch( random ) {
                    case 1:
                    currentTile = tile2_1;
                    break;
                    case 2:
                    currentTile = tile2_2;
                    break;
                    case 3:
                    currentTile = tile2_3;
                    break;
                }
                tilelist.Add(currentTile);
            }
           
        }
    }
    private void drawLevel() {
        Vector3 position= new Vector3(0,0,25);
        Quaternion sameQuart = tile1_1.transform.rotation;
        for (int i= 0; i<4 ; i++) {
            Instantiate(tilelist [i], position + new Vector3(0,0,25), sameQuart );
        }
    }
   
 }

