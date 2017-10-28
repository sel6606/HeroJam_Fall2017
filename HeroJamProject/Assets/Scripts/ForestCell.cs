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
    public GameObject firePrefab;

    private bool onFire;
    private bool setOnFire;


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

    private void LateUpdate()
    {
        if (setOnFire)
        {
            onFire = true;
            setOnFire = false;
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
        GameObject fire = Instantiate(firePrefab);
        fire.transform.parent = gameObject.transform;
        fire.transform.localPosition = Vector3.zero;
    }

}
