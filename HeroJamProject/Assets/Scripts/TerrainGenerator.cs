using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{
	private TerrainData myTerrainData;//Information of the terrain
	public Vector3 worldSize;//Size of the terrain
	public int resolution = 513;//resolution of the heightmap

	public Texture2D myTexture;//texture to apply to the terrain

	public float timeStep = 0.01f; //Time Step for PerlinNoise
	public float startTime = 0.0f; //Start Time for PerlinNoise

	//Operational mode
	public enum MODE
	{
		PLANE,
		RAMP,
		PERLIN,
		WAVE
	}
	public MODE scene = MODE.PLANE; //Current scene
	private MODE lastSscene = MODE.PLANE; //last scene displayed

	// Use this for initialization
	void Start ()
	{
		//Get the terrain collider of the terrain to acces information of it
		TerrainCollider terrainCollider = gameObject.GetComponent<TerrainCollider> ();

		//Error control
		if (terrainCollider == null)
		{
			Debug.Log("Could not get TerrainCollider from Terrain object");
			Debug.Break();//Stop execution
		}

		myTerrainData = terrainCollider.terrainData; //set the data variable
		myTerrainData.size = worldSize; //set the size based on the editor
		myTerrainData.heightmapResolution = resolution; //set the resolution based on the editor
		
		//Select the secene
		switch (scene)
		{
		case MODE.PLANE:
			GeneratePlane ();
			break;
		case MODE.RAMP:
			GenerateRamp ();
			break;
		case MODE.PERLIN:
			GeneratePerlin ();
			break;
		case MODE.WAVE:
			GeneratePerlin ();
			break;
		}
		
		//a SplatPrototype is the brush stencil to be applied in the terrain, in this case only one
		SplatPrototype[] terrainTexture = new SplatPrototype[1]; 
		terrainTexture [0] = new SplatPrototype (); 
		terrainTexture [0].texture = myTexture; 
		myTerrainData.splatPrototypes = terrainTexture;

		//If the resolution is larger than 1K force it 1K the larger the terrain the more time it takes to fill
		//the height map, height maps are prefered in powers of 2 + 1
		if (resolution > 513)
			resolution = 513;
	}

	//Generate a flat terrain
	void GeneratePlane()
	{
		//start a new array to store the information
		float[,] heights = new float[resolution,resolution];
		//fill the array through a nested loop
		for(uint z = 0; z < resolution; z++)
		{
			for(uint x = 0; x < resolution; x++)
			{
				heights[x,z] = 0.1f;
			}
		}
		//set the array to the terrain data
		myTerrainData.SetHeights(0,0,heights);
	}

	//Generate a ramp that is growing front to back
	void GenerateRamp()
	{
		//start a new array to store the information
		float[,] heights = new float[resolution,resolution];
		//fill the array through a nested loop
		for(uint z = 0; z < resolution; z++)
		{
			for(uint x = 0; x < resolution; x++)
			{
				// at x = 0 value is 0 at x =  resolution value is 1, a ramp
				heights[x,z] = (float)x / (float)resolution; 
			}
		}
		//set the array to the terrain data
		myTerrainData.SetHeights(0,0,heights);
	}

	//Generate waves
	void GeneratePerlin()
	{
		// local variables to hold X and Y values
		float xVal = 1f + startTime;
		float yVal = 1f + startTime;

		//create a 2D array to store height information
		float[,] heights = new float[resolution, resolution];

		//Calculate values based on the perlin noise function
		for (int y = 0; y < resolution; y++)
		{
			yVal = yVal + timeStep;
			xVal = 1f;
			
			for (int x = 0; x < resolution; x++)
			{
				xVal = xVal + timeStep;
				float sampleVal = Mathf.PerlinNoise (xVal, yVal);
				heights [x, y] = sampleVal;
			}
		}

		//Once the values are calculated comunicate them to the terrain information
		myTerrainData.SetHeights (0, 0, heights);
	}

	//returns the height of the terrain at the specified position
	public float GetHeight(Vector3 position)
	{
		return Terrain.activeTerrain.SampleHeight(position);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//If the last scene is different than the current one update the terrain
		if (lastSscene != scene)
		{
			switch (scene)
			{
			case MODE.PLANE:
				GeneratePlane ();
				break;
			case MODE.RAMP:
				GenerateRamp ();
				break;
			case MODE.PERLIN:
				GeneratePerlin ();
				break;
			}
		}

		//If the scene is wave update the terrain each frame
		if (MODE.WAVE == scene)
		{
			GeneratePerlin ();
			startTime += 0.01f; //increment so it moves
		}

		lastSscene = scene; //indicate which was the last scene
	}
}