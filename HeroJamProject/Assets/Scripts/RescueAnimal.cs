using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RescueAnimal : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// Unity's OnTriggerEnter method that checks for a trigger collider and then does something
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // if the player collided with the animal
        if(other.gameObject.tag == "Player")
        {
            // increment the counter
            GameInfo.instance.FoxCount++;

            // make them go away for now
            Destroy(gameObject);
        }
    }
}
