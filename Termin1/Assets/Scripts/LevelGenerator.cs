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
    private int t1 ;
    private int t2;

    //Constants
    const int TILES_STARTCOUNT = 4;
    const int TILE_DISTANCE = 50;
    Quaternion ROTATION = new Quaternion(0, 0, 0, 0);
    const int DISTANCE_TO_NEXT_FREE_POSITION = TILES_STARTCOUNT * TILE_DISTANCE;

    void Start ()
    {
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
        position += new Vector3(0, 0, DISTANCE_TO_NEXT_FREE_POSITION);
        addTile(position);
    }

    /*
     * Destroys all Tiles in tilelist and creates new Level (used in Respawn)
     */
    public void newLevel () {
        removeAllTiles();
        generateLevel();
    }

#endregion

#region PrivateFunctions

    /*
     * Generates 4 Tiles in Row (used at Start and Respawn)
     */
    private void generateLevel() {
        Vector3 position = new Vector3(0, 0, 0); 
        
        for (int i=0; i < TILES_STARTCOUNT; i++){
            if( i % 2 == 0 )   addT1 = true;
            else               addT1 = false;

            addTile(position);
            position += new Vector3(0, 0, TILE_DISTANCE);
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

    /*
     * Destroys all Instances of Tiles(GameObjects) in tilelist and clears List
     */
     private void removeAllTiles() {
        foreach( GameObject tile in tilelist ) {
            GameObject.Destroy(tile);
        }
        tilelist.Clear();
    }
 #endregion

}

