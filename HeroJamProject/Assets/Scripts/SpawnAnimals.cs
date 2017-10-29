using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnAnimals : MonoBehaviour {

    // take a reference to a prefab to spawn
    public GameObject animal;

    // get a reference to the fox icons
    public RawImage fox1;
    public RawImage fox2;
    public RawImage fox3;

    public Texture foxCollected;
    public Texture foxDanger;

    // get a reference to the rescue animal script
    public RescueAnimal rescueScript;

    // get a reference to the forest cell positions
    public GameObject forestMnger;
    private ForestGenerator forestGen;

    private ForestCell spawnCell;

    // create intVec2's for random placement
    private float xSpawn;
    private float ySpawn;
    IntVector2 spawnPos;

    // create a list of intVector2's to keep track of current spawn positions of animals
    // and make sure that none of them are the same
    List<IntVector2> animalSpawns;

    private bool samePos = false;

    public int animalCount = 0;
    private int foxCount = 10;

	// Use this for initialization
	void Start ()
    {
        // referece to forestGen script
        forestGen = forestMnger.GetComponent<ForestGenerator>();
        rescueScript = animal.GetComponent<RescueAnimal>();

        // reset the icons to false so that they don't show up as rescued at the start
        fox1.enabled = false;
        fox2.enabled = false;
        fox3.enabled = false;

        GameInfo.instance.FoxCount = 0;

        // instantiate the list
        animalSpawns = new List<IntVector2>();
    }

    // Update is called once per frame
    void Update ()
    {
        // spawn animals
        if(animalCount<foxCount)
        {
            SpawnAnimal();
        }

        // match the images with the gameObjects
        if (GameInfo.instance.FoxCount == 1)
        {
            fox1.enabled = true;
        }
        // match the images with the gameObjects
        if (GameInfo.instance.FoxCount == 2)
        {
            fox2.enabled = true;
        }
        // match the images with the gameObjects
        if (GameInfo.instance.FoxCount == 3)
        {
            fox3.enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("LevelComplete");
        }
    }
    /// <summary>
    /// Method to spawn an animal into the world
    /// </summary>
    void SpawnAnimal()
    {
        // generate random ints
        xSpawn = Random.Range(0, forestGen.sizeX);
        ySpawn = Random.Range(0, forestGen.sizeX);

        // create the spawn position
        spawnPos = new IntVector2((int)xSpawn, (int)ySpawn);

        // get the cell in which the animal should spawn
        spawnCell = forestGen.GetCell(spawnPos);

        // check to make sure the position is unique
        for(int i = 0; i < animalSpawns.Count; i++)
        {
            if (spawnPos == animalSpawns[i])
            {
                samePos = true;
            }
        }

        // add it to the list of spawnPositions
        animalSpawns.Add(spawnPos);

        if (samePos == false)
        {
            // check to make sure the tile isn't already on fire
            if (spawnCell.OnFire == false)
            {
                // get the world position of the forest cell
                Vector3 tempSpawn = new Vector3(spawnCell.transform.position.x, spawnCell.transform.position.y + 0.2f, spawnCell.transform.position.z);

                // instantiate the object in the world on the cell
                GameObject tempObj = Instantiate(animal, tempSpawn, Quaternion.identity);

                // increment the animal spawn counter
                animalCount++;
            }
        }
    }
}
