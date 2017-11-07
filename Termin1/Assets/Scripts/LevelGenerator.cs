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
    private List<GameObject> tilelist ;

    //Cycle: T1,T2,T1,...
    private bool addT1 = true;
    //Length of tilesets
    private int t1 = 0;
    private int t2 = 0;

    //Constants
    int TILEDISTANCE = 50;
    int DISTANCETOEND = 200;
    Quaternion ROTATION = new Quaternion(0, 0, 0, 0);
   
    void Start () {
        tilelist = new List<GameObject>();
        generateLevel();
        t1 = tileSet1.Length;
        t2 = tileSet2.Length;
	}

#region PublicFunctions

    /*
     * Instantiates a new Tile at the End of the Path
     */
    public void newTile (Vector3 position) {
        position += new Vector3(0, 0, DISTANCETOEND);
        addTile(position);
    }

    /*
     * Destroys all Tiles in tilelist and creates new Level (used in Respawn)
     */
    public void destroyTiles () {
        foreach( GameObject tile in tilelist ) {
            GameObject.Destroy(tile);
        }
        tilelist.Clear();
        generateLevel();
    }

#endregion

#region PrivateFunctions

    /*
     * Generates 4 Tiles in Row (used at Start and Respawn)
     */
    private void generateLevel() {
        Vector3 position = new Vector3(0, 0, 0); 
        
        for (int i=0; i<4; i++){
            if( i % 2 == 0 )   addT1 = true;
            else               addT1 = false;

            addTile(position);
            position += new Vector3(0, 0, TILEDISTANCE);
        }
    }

    /*
     * Instantiates Tile and adds it to tilelist
     */
    private void addTile(Vector3 position) {
        int random = 0;
        GameObject temp = null;

        if( addT1 ) {
            random = Random.Range(0, t1);
            temp = Instantiate(tileSet1[random], position, ROTATION);
            addT1 = false;
        }
        else {
            random = Random.Range(0, t2);
            temp = Instantiate(tileSet2[random], position, ROTATION);
            addT1 = true;
        }

        temp.SetActive(true);
        tilelist.Add(temp);
    }
 #endregion

}

