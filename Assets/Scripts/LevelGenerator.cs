using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages Level Generation, Adds and Removes Tiles, holds Referenzes to Tile Prefabs
/// 
/// The chance to spawn a hard tileset is 0% until the lowerDifficultyPointLimit is reached, 
/// then it increases with rising score and is at 100% when the upperPointLimit is reached (and then stays at 100%)
/// 
/// Author: Melanie Ramsch
/// </summary>
public class LevelGenerator : MonoBehaviour {

    [Header("Variables")]
    public int lowerDifficultyPointLimit = 400;
    public int upperDifficultyPointLimit = 1200;

    [Header("StartArea")]
    public GameObject startArea;

    //Tiles
    [Header("Tilesets")]
    public GameObject[] tileSet1;//T1
    public GameObject[] tileSet2;//T2
    public GameObject[] tileSet1Hard;//T1
    public GameObject[] tileSet2Hard;//T2

    //Listof all Tiles in Scene
    private List<GameObject> tilelist;

    //Tiles parent
    private Transform environmentParent;

    //Cycle: T1,T2,T1,...
    private bool addT1 = true;

    //Constants
    public const int TILES_STARTCOUNT = 2;
    const int TILE_DISTANCE = 50;
    Quaternion ROTATION = new Quaternion(0, 0, 0, 0);
    const int DISTANCE_TO_NEXT_FREE_POSITION = TILES_STARTCOUNT * TILE_DISTANCE;

    // Static singleton property
    public static LevelGenerator Instance { get; private set; }

    private int score;
    private int completedTiles=0;

    #region GetterSetter
    public int GetCompletedTiles() {
        return this.completedTiles;
    }

    public void SetCompletedTiles(int value) {
        completedTiles = value;
    }
    #endregion


    void Awake ()
    {
        Instance = this;
    }

    void Start ()
    {
        tilelist = new List<GameObject>();
        environmentParent = GameObject.FindGameObjectWithTag(Constants.TAG_ENVIRONMENT_PARENT).transform;
        GenerateLevel();
        completedTiles = 0;
    }

    private void Update()
    {
        score = ScoreCounter.Instance.Score1 + ScoreCounter.Instance.Distance1;
    }

    #region PublicFunctions

    public void AddNewTile (Vector3 position) {
        position += new Vector3(0, 0, DISTANCE_TO_NEXT_FREE_POSITION);
        InstantiateTile(position);
    }

    //Removes first Tile from tilelist and destroys GameObject
    public void DeleteOldTile () {
        GameObject.Destroy(tilelist [0]);
        tilelist.RemoveAt(0);
    }

	// Destroys all Tiles in tilelist and creates new Level (used in Respawn)
    public void NewLevel () {
        RemoveAllTiles();
        RemoveStartArea();
        SetCompletedTiles(0);
        GenerateLevel();
    }

	#endregion

	#region PrivateFunctions

	//Generates TILES_STARTCOUNT Tiles in Row 
	private void GenerateLevel() {
        Vector3 position = new Vector3(0, 0, 0);
        score = 0;
        Instantiate(startArea, environmentParent);
        for (int i=0; i < TILES_STARTCOUNT; i++){
            if( i % 2 == 0 )   addT1 = true;
            else               addT1 = false;

            InstantiateTile(position);
            position += new Vector3(0, 0, TILE_DISTANCE);
        }
    }

    private void InstantiateTile(Vector3 position) {
        int random = 0;
        GameObject temp = null;

        if( addT1 ) {
            // Chance to spawn a hard tileset is 0% until the lowerDifficultyPointLimit is reached, 
            // then increases with rising score and is at 100% when the upperPointLimit is reached
            if (score + Random.Range(1, upperDifficultyPointLimit - lowerDifficultyPointLimit) > upperDifficultyPointLimit) {
                random = Random.Range(0, tileSet1Hard.Length);
                temp = Instantiate(tileSet1Hard[random], position, ROTATION, environmentParent);
            }
            else
            {
                random = Random.Range(0, tileSet1.Length);
                temp = Instantiate(tileSet1[random], position, ROTATION, environmentParent);
            }
            addT1 = false;
        }
        else {
            // Chance to spawn a hard tileset is 0% until the lowerDifficultyPointLimit is reached, 
            // then increases with rising score and is at 100% when the upperPointLimit is reached
            if (score + Random.Range(1, upperDifficultyPointLimit - lowerDifficultyPointLimit) > upperDifficultyPointLimit)
            {
                random = Random.Range(0, tileSet2Hard.Length);
                temp = Instantiate(tileSet2Hard[random], position, ROTATION, environmentParent);
            }
            else
            {
                random = Random.Range(0, tileSet2.Length);
                temp = Instantiate(tileSet2[random], position, ROTATION, environmentParent);
            }
            addT1 = true;
        }

        temp.SetActive(true);
        tilelist.Add(temp);
    }

    private void RemoveAllTiles () {
        foreach( GameObject tile in tilelist ) {
            GameObject.Destroy(tile);
        }
        tilelist.Clear();
    }

    private void RemoveStartArea() {
        if( GameObject.FindGameObjectWithTag(Constants.TAG_START_AREA) )
            Destroy(GameObject.FindGameObjectWithTag(Constants.TAG_START_AREA));
    }
    #endregion

}

