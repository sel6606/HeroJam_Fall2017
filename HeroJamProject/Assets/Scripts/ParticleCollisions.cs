using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisions : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    private void OnParticleCollision(GameObject other)
    {
        if (other.tag.Equals("Cell"))
        {
            if(other.GetComponentInParent<ForestCell>().OnFire)
            {
                other.GetComponentInParent<ForestCell>().Extinguish();
            }
        }
    }
}
