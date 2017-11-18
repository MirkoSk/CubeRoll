using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour {

    [Header("Variables")]
    public int difficultyPointLimit = 1000;

    //Tiles
    [Header("Tilesets")]
    public GameObject[] tileSet1;//T1
    public GameObject[] tileSet2;//T2
    public GameObject[] tileSet1Hard;//T1
    public GameObject[] tileSet2Hard;//T2

    //Listof all Tiles in Scene
    private List<GameObject> tilelist ;

    //Cycle: T1,T2,T1,...
    private bool addT1 = true;

    //Constants
    const int TILES_STARTCOUNT = 4;
    const int TILE_DISTANCE = 50;
    Quaternion ROTATION = new Quaternion(0, 0, 0, 0);
    const int DISTANCE_TO_NEXT_FREE_POSITION = TILES_STARTCOUNT * TILE_DISTANCE;

    // Static singleton property
    public static LevelGenerator Instance { get; private set; }

    private int score;

    void Awake()
    {
        // Save a reference to the AudioHandler component as our singleton instance
        Instance = this;
    }

    void Start ()
    {
        tilelist = new List<GameObject>();
        generateLevel();
    }

    private void Update()
    {
        score = ScoreCounter.Instance.score;
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
        score = 0;
        
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
            if (score < difficultyPointLimit)
            {
                random = Random.Range(0, tileSet1.Length);
                temp = Instantiate(tileSet1[random], position, ROTATION);
            }
            else {
                random = Random.Range(0, tileSet1Hard.Length);
                temp = Instantiate(tileSet1Hard[random], position, ROTATION);
            }
            addT1 = false;
        }
        else {
            if (score < difficultyPointLimit)
            {
                random = Random.Range(0, tileSet2.Length);
                temp = Instantiate(tileSet2[random], position, ROTATION);
            }
            else {
                random = Random.Range(0, tileSet2Hard.Length);
                temp = Instantiate(tileSet2Hard[random], position, ROTATION);
            }
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

