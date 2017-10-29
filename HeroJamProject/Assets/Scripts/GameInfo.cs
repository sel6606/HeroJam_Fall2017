using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton that holds various info that can be accessed from anywhere
/// </summary>
public class GameInfo : MonoBehaviour
{
    //Represents the game info that is stored across all scenes
    public static GameInfo instance;

    #region Class Variables
    private bool forestGenerated;
    private float burnChance;
    private int foxCount;
    private bool paused;
    #endregion

    #region Properties
    public bool ForestGenerated
    {
        get { return forestGenerated; }
        set { forestGenerated = value; }
    }

    public int FoxCount
    {
        get { return foxCount; }
        set { foxCount = value; }
    }

    public float BurnChance
    {
        get { return burnChance; }
        set { burnChance = value; }
    }

    public bool Paused
    {
        get { return paused; }
        set { paused = value; }
    }
    #endregion

    private void Awake()
    {
        //If there is not already a GameInfo object, set it to this
        if(instance == null)
        {
            //Object this is attached to will be preserved between scenes
            DontDestroyOnLoad(gameObject);

            instance = this;
        }
        else if(instance != this)
        {
            //Ensures that there are no duplicate objects being made every time the scene is loaded
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        //Initializes either the default values or the values from the previous scene
        forestGenerated = instance.ForestGenerated;
        burnChance = instance.BurnChance;
        foxCount = instance.FoxCount;
        paused = instance.Paused;
	}
	
}
