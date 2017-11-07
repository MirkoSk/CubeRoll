using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour {

    //Tiles
    [Header("Tileset 1")]
    public GameObject[] tileSet1;//T1
    
    [Header("Tileset 2")]
    public GameObject[] tileSet2;//T2
   

    //Listof all Tiles in Scene
    List<GameObject> tilelist ;

    //Cycle: T1,T2,T1,...
    private bool addT1 = true;


    void Start () {
        tilelist = new List<GameObject>();
        generateLevel();
	}
	
    public void addNewTile (Vector3 position, Quaternion rotation) {
        position += new Vector3(0, 0, 200);
        int random = Random.Range(0, 3);
        GameObject temp=null;

        if( !addT1 ) {
            temp = Instantiate(tileSet1[random],position, rotation);
            addT1 = true;
        }
        else {
            temp = Instantiate(tileSet2[random], position, rotation);
            addT1 = false;
        }
        temp.SetActive(true);
        tilelist.Add(temp);
    }
    private void generateLevel() {
        GameObject temp = null;
        Vector3 position = new Vector3(0, 0, 0);
        Quaternion rotation = new Quaternion(0,0,0,0);
        int random=0; 
        //adding 4 tiles at the start of the game
        for (int i=0; i<4; i++){
            random = Random.Range(0, 3);
            print("i: "+i+" random number: "+random+" Array length: "+tileSet1.Length);//need it to find bug: for is started a 5th time with i=0...?! sometimes... have to figue it out... 
               
                if( i % 2 == 0 ) {//T1 
                    temp = Instantiate(tileSet1[random], position, rotation);
                    addT1 = true;
                }
                 else{//T2
                    temp = Instantiate(tileSet2[random], position, rotation);
                    addT1 = false;
                }
            
            temp.SetActive(true);
            tilelist.Add(temp);
            position += new Vector3(0, 0, 50);
        }
    }

    public void destroyTiles() {
        //destroys every gameObject in List
        foreach( GameObject tile in tilelist ) {
            GameObject.Destroy(tile);
        }
        tilelist.Clear();
        generateLevel();
    }

 }

