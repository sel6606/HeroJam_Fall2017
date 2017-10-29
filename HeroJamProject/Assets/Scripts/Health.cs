using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    
  

    GameObject manager;
    HoldingVar health;

   
    


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
            
            health.Damage();
        }
       
        
    }

    private void OnParticleTrigger()
    {
        if(gameObject.GetComponent<ForestCell>().OnFire)
        {
            gameObject.GetComponent<ForestCell>().Extinguish();
        }
    }


}
