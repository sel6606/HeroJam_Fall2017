using System.Collections;
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
    public GameObject burnt;
    public GameObject unburnt;
    #endregion

    #region Class Variables
    private bool onFire;
    private bool setOnFire;
    private bool fireExtinguished;

    private GameObject fire;
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
        if(!GameInfo.instance.Paused)
        {
            if (setOnFire)
            {
                onFire = true;
                setOnFire = false;
            }

            if (fireExtinguished)
            {
                onFire = false;
                fireExtinguished = false;
            }
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
        fire = Instantiate(firePrefab);
        fire.transform.parent = gameObject.transform;
        fire.transform.localPosition = Vector3.zero;

        ChangeLayersRecursively(burnt.transform, "Visible");
        ChangeLayersRecursively(unburnt.transform, "Hidden");
    }

    /// <summary>
    /// Extinguishes the fire on this cell
    /// </summary>
    public void Extinguish()
    {
        fireExtinguished = true;
        gameObject.tag = "Untagged";
        Destroy(fire);
    }

    /// <summary>
    /// Recursively changes the layer of a hierarchy of objects
    /// </summary>
    /// <param name="trans">The transform of the top object in the hierarchy</param>
    /// <param name="layer">The layer to change the objects to</param>
    public void ChangeLayersRecursively(Transform trans, string layer)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(layer);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, layer);
        }
    }


}
