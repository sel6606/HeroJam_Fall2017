using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains scripts for the forest cells
/// </summary>
public class ForestCell : MonoBehaviour
{
    public Material burningMat;
    public IntVector2 coordinates;

    private bool onFire;


    public bool OnFire
    {
        get { return onFire; }
        set { onFire = value; }
    }

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    /// <summary>
    /// Sets this cell on fire
    /// </summary>
    public void SetFire()
    {
        onFire = true;
        gameObject.GetComponentInChildren<Renderer>().material = burningMat;
        gameObject.tag = "OnFire";
    }

}
