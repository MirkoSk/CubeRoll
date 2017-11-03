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
    private bool even = true;


    // Use this for initialization
    void Start () {
        generateLevel();
       
        tilelist = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void addNewTile (Vector3 position, Quaternion rotation) {
        position += new Vector3(0, 0, 200);
        int random = Random.Range(0, 2);
        if( even ) {
            switch( random ) {
                case 0:
                Instantiate(tile1_1, position, rotation);
                break;
                case 1:
                Instantiate(tile1_2, position, rotation);
                break;
                case 2:
                Instantiate(tile1_3, position, rotation);
                break;
            }
            even = false;
        }
        else {
            switch( random ) {
                case 0:
                Instantiate(tile2_1, position, rotation);
                break;
                case 1:
                Instantiate(tile2_2, position, rotation);
                break;
                case 2:
                Instantiate(tile2_3, position, rotation);
                break;
            }
            even = true;
        }
    }
    private void generateLevel() {
        //startposition of first Tile
        Vector3 position = new Vector3(0, 0, 0);
        Quaternion sameQuart = tile1_1.transform.rotation;
        //Random Number for choosing Tiles
        int random=0; 
        for (int i=0; i<4; i++){
            //getting random Number
            random = Random.Range(0, 2);
            if( i % 2 == 0 ) {
                switch( random ) {
                    case 0:
                    Instantiate(tile1_1, position, sameQuart);
                    position += new Vector3(0, 0, 50);
                    break;
                    case 1:
                    Instantiate(tile1_2, position, sameQuart);
                    position += new Vector3(0, 0, 50);
                    break;
                    case 2:
                    Instantiate(tile1_3, position, sameQuart);
                    position += new Vector3(0, 0, 50);
                    break;
                } 
             }
            else{
                switch( random ) {
                    case 0:
                    Instantiate(tile2_1, position, sameQuart);
                    position += new Vector3(0, 0, 50);
                    break;
                    case 1:
                    Instantiate(tile2_2, position, sameQuart);
                    position += new Vector3(0, 0, 50);
                    break;
                    case 2:
                    Instantiate(tile2_3, position, sameQuart);
                    position += new Vector3(0, 0, 50);
                    break;
                }
            }
        }
    }
   
 }

