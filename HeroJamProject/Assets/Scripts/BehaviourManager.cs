using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviourManager : MonoBehaviour
{
	public GameObject vehicle; //vehicle to control
	public GameObject target; //target to seek/flee

	public GameObject vehiclePrototype; //vehicle to instantiate
	public GameObject targetPrototype; //target to instantiate

	public Terrain terrain; //terrain we are walking in
	private TerrainGenerator terrainGenerator; //terrain information
	public Vector3 worldSize; //world size

	private BoundingSphere vehicleBS; //bounding sphere of the vehicle
	private BoundingSphere targetBS; //bounding sphere of the target

	public bool seekingFleeing = false;

	public  List<GameObject> seekers;
	public float seekerCount = 0;

	//Materials
	public Material matRed;
	public Material matGreen;
	public Material matBlue;
	public Material matWhite;
	public Material matYellow;
	public Material matPink;

	// Use this for initialization
	void Start ()
	{
		seekers = new List<GameObject> ();
		//is vehicle prototype assigned in editor?
		if(null == vehiclePrototype)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": VehiclePrototype is not assigned.");
			Debug.Break();
		}
		//is target prototype assigned in editor?
		if(null == targetPrototype)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": VehiclePrototype is not assigned.");
			Debug.Break();
		}
		//is terrain assigned in editor?
		if(null == terrain)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": Terrain is not assigned.");
			Debug.Break();
		}
		//is the terrain assigned a terraingenerator component?
		terrainGenerator = terrain.GetComponent<TerrainGenerator>();
		if(null == terrainGenerator)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": Terrain is required to have a TerrainGenerator script");
			Debug.Break();
		}

		//instantiate vehicle
		vehicle = Instantiate(vehiclePrototype);
		//instantiate tareget
		target = Instantiate(targetPrototype);
		//initialize this world size with the terrain generator world size
		worldSize = terrainGenerator.worldSize;

		//initialize vehicle bounding sphere with vehicle component
		vehicleBS = vehicle.GetComponent<BoundingSphere>();
		//check is assigned
		if(null == vehicleBS)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": VehiclePrototype is required to have a Bounding Sphere script");
			Debug.Break();
		}
		//initialize target bounding sphere with target component
		targetBS = target.GetComponent<BoundingSphere>();

		//check if assigned
		if(null == targetBS)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": TargetPrototype is required to have a Bounding Sphere script");
			Debug.Break();
		}
		//initialize movementForces with vehicle's component
		MovementForces vehicleMF = vehicle.GetComponent<MovementForces>();
		if(null == vehicleMF)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": VehiclePrototype is required to have a MovementForces script");
			Debug.Break();
		}
		//initialize movementForces with vehicle's component
		MovementForces targetMF = target.GetComponent<MovementForces>();
		if(null == targetMF)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": TargetPrototype is required to have a MovementForces script");
			Debug.Break();
		}

		//Set the target of the vehicle to the target object
		vehicleMF.SetTarget(target);

		//Set the target of the vehicle to the target object
		targetMF.SetTarget(vehicle);

		//Randomize vehicle's position
		RandomizePosition(vehicle);
		//randomize target's position
		RandomizePosition(target);
	}

	//calculates the position of the object at random
	void RandomizePosition(GameObject theObject)
	{
		//Set position of target based on the size of the world
		Vector3 position = new Vector3 (Random.Range(0.0f,100.0f), 0.0f, Random.Range(0.0f,100.0f));
		//set the height of the object based on the position of the terrain
		position.y = terrainGenerator.GetHeight(position) + 1.0f;
		//set the position of target back
		theObject.transform.position = position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Check for collision
		if(vehicleBS.IsColliding(targetBS))
		{
			RandomizePosition(target);
		}
	}
}
