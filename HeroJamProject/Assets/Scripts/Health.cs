using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    
  

    GameObject manager;
    HoldingVar health;

   
    bool onFire;


	// Use this for initialization
	void Start ()
    {
       
        manager = GameObject.Find("SceneManager");
        health = manager.GetComponent<HoldingVar>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        
	}

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.tag == "OnFire") 
        {
            onFire = true;
            health.Damage();
        }
        else
        {
            onFire = false;
        }
        //Debug.Log(onFire);
    }

    private void OnParticleTrigger()
    {
        Debug.Log("IT WORKED FUCK YEAH");
        if(gameObject.GetComponent<ForestCell>().OnFire)
        {
            gameObject.GetComponent<ForestCell>().Extinguish();
        }
    }


}
