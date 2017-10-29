using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scripts for generating a forest
/// </summary>
public class ForestGenerator : MonoBehaviour
{
    public ForestCell cellPrefab;
    public GameObject playerPrefab;
    public GameObject sManager;


    public GameObject[] walls;
    public BoxCollider[] wallCol;
    

    public int sizeX;
    public int sizeZ;

    public bool increaseSpeed;
    public float increaseSpeedTime;

    public float burnChance;

    private ForestCell[,] cells;
    private float timeElapsed;

    private Texture2D perlinTex;
    public Texture2D tex1;
    public Texture2D tex2;
    private float startTime = 0.0f;
    private float step = 0.1f;

	// Use this for initialization
	void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameInfo.instance.BurnChance = burnChance;

        perlinTex = new Texture2D(sizeX, sizeZ);
        Color[] pixelValues = new Color[sizeX * sizeZ];
        float xCoord, yCoord = startTime;
        for (int i = 0; i < sizeX; i++)
        {
            xCoord = startTime;
            for (int j = 0; j < sizeZ; j++)
            {
                float value = Mathf.PerlinNoise(xCoord, yCoord);
                //Debug.Log(value);
                pixelValues[i * sizeZ + j] = new Color(value, value, value);
                xCoord += step;
            }
            yCoord += step;
        }
        perlinTex.SetPixels(pixelValues);
        perlinTex.Apply();
        

        GenerateForest();
        timeElapsed = 0;

        wallCol = new BoxCollider[4];
        for (int i = 0; i < 4; i++)
        {
            wallCol[i] = walls[i].GetComponent<BoxCollider>();
        }
        GenerateWalls();

        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(GameInfo.instance.Paused)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= increaseSpeedTime && increaseSpeed)
            {
                GameInfo.instance.BurnChance += 0.01f;
                timeElapsed = 0.0f;
            }
        }
	}

    /// <summary>
    /// Returns a cell at the specified coordinates
    /// </summary>
    /// <param name="coordinates">The coordinates of the cell</param>
    /// <returns>The cell</returns>
    public ForestCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.y];
    }

    /// <summary>
    /// Generates each cell in the forest
    /// </summary>
    public void GenerateForest()
    {
        cells = new ForestCell[sizeX, sizeZ];

        //Create the cells
        for(int i = 0; i < sizeX; i++)
        {
            for(int j=0; j < sizeZ; j++)
            {
                CreateCell(i, j);
            }
        }


        //Set fires on 4 random cells
        IntVector2 randomFire;
        for(int i = 0; i < 4; i++)
        {
            randomFire = new IntVector2(Random.Range(0, sizeX), Random.Range(0, sizeZ));
            GetCell(randomFire).SetFire();
        }

        //Tell GameInfo that the forest has now been completely generated
        GameInfo.instance.ForestGenerated = true;

        GameObject tempPlayer = Instantiate(playerPrefab);
        tempPlayer.transform.position = new Vector3(0, 2, 0);


    }


    /// <summary>
    /// Takes the 4 invisble wall objects resizes them and postitions them to stop players from leaveing the map.
    /// </summary>
    void GenerateWalls()
    {
       
            
       
        walls[0].transform.position = new Vector3(0, 1, sizeZ);
        wallCol[0].size = new Vector3(sizeX * 2, 1, 1);

        walls[1].transform.position = new Vector3(0, 1, -sizeZ);
        wallCol[1].size = new Vector3(sizeX * 2, 1, 1);

        walls[2].transform.position = new Vector3(sizeX, 1, 0);
        wallCol[2].size = new Vector3(1, 1,sizeZ * 2);

        walls[3].transform.position = new Vector3(-sizeX, 1, 0);
        wallCol[3].size = new Vector3(1, 1, sizeZ * 2);
    }


    /// <summary>
    /// Creates a cell at the specified coordinates
    /// </summary>
    /// <param name="xPos">The x coordinate</param>
    /// <param name="zPos">The z coordinate</param>
    /// <returns>The new cell</returns>
    private ForestCell CreateCell(int xPos, int zPos)
    {
        //Instantiate a new cell
        ForestCell newCell = Instantiate(cellPrefab);
        cells[xPos, zPos] = newCell;

        //Name the cell
        newCell.name = "Cell " + xPos + ", " + zPos;

        //Set the transform of the cell to be centered at the forest object
        newCell.transform.parent = transform;

        //Set the coordinates of the cell
        newCell.coordinates = new IntVector2(xPos, zPos);

        //Calculate the position of the cell in the forest
        Vector3 cellPos = new Vector3(xPos - sizeX * 0.5f + 0.5f, 0f, zPos - sizeZ * 0.5f + 0.5f);
        cellPos *= 2;
        newCell.transform.localPosition = cellPos;
        
        Material materials = newCell.GetComponentsInChildren<Renderer>()[0].material;
        Debug.Log(perlinTex.GetPixel(xPos, zPos).grayscale);
        if (materials != null)
        {
            if (perlinTex.GetPixel(xPos, zPos).grayscale <= 0.5f)
            {
                materials.mainTexture = tex1;
            }
            else materials.mainTexture = tex2;
        }
        

        newCell.GetComponent<Health>().manager = sManager;

        return newCell;
    }

    /// <summary>
    /// Checks whether the given coordinates fall within the bounds of the forest
    /// </summary>
    /// <param name="coordinate">The coordinates to check</param>
    /// <returns>True or false</returns>
    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        if (coordinate.x >= 0 && coordinate.x < sizeX && coordinate.y >= 0 && coordinate.y < sizeZ)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
