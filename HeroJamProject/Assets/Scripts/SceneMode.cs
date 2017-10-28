using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneMode : MonoBehaviour
{
	public GameObject terrainObject; //Terrain from the editor
	private TerrainGenerator terrainGenerator; //TerrainGenerator Script

	// Use this for initialization
	void Start ()
	{
		//Error control
		if (terrainObject == null)
		{
			Debug.Log("Terrain Object not assigned in GameManager");
			Debug.Break();//Stop execution
		}

		//Get the TerrainGenerator script component from the terrain
		terrainGenerator = terrainObject.GetComponent<TerrainGenerator> ();
		//Error control
		if (terrainGenerator == null)
		{
			Debug.Log("TerrainGenerator script not assigned to a Terrain");
			Debug.Break();//Stop execution
		}

	}

	//GUI
	void OnGUI()
	{
		//show the options
		//GUILayout.Box("Press 1 through 4 to switch scene, left click to shoot, escape to finish");
		if (Event.current.Equals(Event.KeyboardEvent("1")))
			terrainGenerator.scene = TerrainGenerator.MODE.PLANE;
		if (Event.current.Equals(Event.KeyboardEvent("2")))
			terrainGenerator.scene = TerrainGenerator.MODE.RAMP;
		if (Event.current.Equals(Event.KeyboardEvent("3")))
			terrainGenerator.scene = TerrainGenerator.MODE.PERLIN;
		if (Event.current.Equals(Event.KeyboardEvent("4")))
			terrainGenerator.scene = TerrainGenerator.MODE.WAVE;
		if (Event.current.Equals(Event.KeyboardEvent("escape")))
			Application.Quit();
	}

	// Update is called once per frame
	void Update ()
	{
	}
}
