using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains scripts for the forest cells
/// </summary>
public class ForestCell : MonoBehaviour
{
    public Material burningMat;
    public Material burnedMat;
    public IntVector2 coordinates;

    private bool onFire;
    private bool burnedOut;
    private bool justBurnedOut;
    private bool setOnFire;


    public bool OnFire
    {
        get { return onFire; }
        set { onFire = value; }
    }

    public bool BurnedOut
    {
        get { return burnedOut; }
        set { burnedOut = value; }
    }

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void LateUpdate()
    {
        if (setOnFire)
        {
            onFire = true;
            setOnFire = false;
        }

        if(justBurnedOut)
        {
            onFire = false;
            burnedOut = true;
        }
    }

    /// <summary>
    /// Sets this cell on fire
    /// </summary>
    public void SetFire()
    {
        setOnFire = true;
        gameObject.GetComponentInChildren<Renderer>().material = burningMat;
        gameObject.tag = "OnFire";
    }

    /// <summary>
    /// Marks this tile as burned out
    /// </summary>
    public void BurnOut()
    {
        justBurnedOut = true;
        gameObject.GetComponentInChildren<Renderer>().material = burnedMat;
        gameObject.tag = "BurnedOut";

    }

}
