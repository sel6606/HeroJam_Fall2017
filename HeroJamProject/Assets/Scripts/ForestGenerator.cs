﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scripts for generating a forest
/// </summary>
public class ForestGenerator : MonoBehaviour
{
    public ForestCell cellPrefab;

    public int sizeX;
    public int sizeZ;

    private ForestCell[,] cells;

	// Use this for initialization
	void Start ()
    {
        GenerateForest();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
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

        for(int i = 0; i < sizeX; i++)
        {
            for(int j=0; j < sizeZ; j++)
            {
                CreateCell(i, j);
            }
        }

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