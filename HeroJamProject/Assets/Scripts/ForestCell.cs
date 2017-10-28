﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains scripts for the forest cells
/// </summary>
public class ForestCell : MonoBehaviour
{
    #region Public Variables
    public Material burningMat;
    public Material burnedMat;
    public IntVector2 coordinates;
    public GameObject firePrefab;
    #endregion

    #region Class Variables
    private bool onFire;
    private bool setOnFire;
    #endregion

    #region Properties
    public bool OnFire
    {
        get { return onFire; }
        set { onFire = value; }
    }
    #endregion

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Called at the end of the frame
    /// </summary>
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

        //Instantiate a fire and parent it to this cell
        GameObject fire = Instantiate(firePrefab);
        fire.transform.parent = gameObject.transform;
        fire.transform.localPosition = Vector3.zero;
    }

}
