using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingVar : MonoBehaviour {

    public float health;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(health);
	}

    public void Damage()
    {
        health -= Time.deltaTime;
        health = Mathf.Clamp(health, 0f, 1000f);

    }
}
