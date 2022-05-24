using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs; //list of objects, where we put the different tiles into. 
    public float zSpawn = 0; //only moving the tiles at the z-axis. y- and x-axis won't be changed. 
    public float tileLength = 40; //if have estimated that each tile is 40
    public int numberofTiles = 3; //this is how many tiles we want to loop through at a time. 
    private List<GameObject> activeTiles = new List<GameObject>(); //creating a list so it removes used tiles

    public Transform playerTransform;

    void Start()
    {
        for (int i = 0; i < numberofTiles; i++) //looping through tiles
        {
            if (i == 0)
                SpawnTile(0); //as default. The first tile should be the one without obstacles. 
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length)); //randomly chosen. 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z -35 > zSpawn - (numberofTiles * tileLength)) //checking the playerposition and comparing it, so it knows when to loop and add more tiles. 
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile(); //deleting tiles that we have passed. 
        }
    }

    public void SpawnTile(int tileIndex) //function that instanciates the tile loop and adds more
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile() //function that destroys used tiles. 
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
