using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    CharacterController Character;
    GameObject player;

    GameObject manager;
    HoldingVar health;

    BoxCollider box;
    bool onFire;


	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        manager = GameObject.Find("SceneManager");
        health = manager.GetComponent<HoldingVar>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        OnTriggerStay(player.GetComponent<Collider>());
	}

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.tag == "OnFire") 
        {
            onFire = true;
        }
        else
        {
            onFire = false;
        }
        Debug.Log(onFire);
    }
}
