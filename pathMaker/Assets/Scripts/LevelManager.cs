using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Basics of Object Oriented Programming is to make anything private that you can
    [SerializeField]
    private GameObject[] tilePrefabs;

    public float TileSize
    {
    	// Allow us to return the value of tileSize later
    	get {return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevel()
    {
    	string[] mapData = ReadLevelText();

    	// stores length of the string on the first line as columns
    	int mapX = mapData[0].ToCharArray().Length;
    	// stores the length of the array as how many rows
    	int mapY = mapData.Length;

    	// Gets "0,0" from the main camera
    	Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height));
    	
    	for (int y = 0; y < mapY; y++) // the rows
    	{
    		
    		char[] newTiles = mapData[y].ToCharArray();

    		for(int x = 0; x < mapX; x++) // the columns
    		{
    			PlaceTile(newTiles[x].ToString(), x, y, worldStart);
    		}
    	}
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
    			// "0" = grass "2" = sand "3" water
    			int tileIndex = int.Parse(tileType);

    			// Creates a new tile object that we can reference
	    		GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
	    		// Uses the new tile variable to change the position of the tile
	    		newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        // remove \n from the file
        string tmpData = bindData.text.Replace(Environment.NewLine, string.Empty);

        return tmpData.Split('-');
    }
}
