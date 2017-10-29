using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    public float timer;
    public float score;
    public float health;

    public bool levelCom;
    public bool mainS;
    public bool gOver;
	// Use this for initialization
	void Start ()
    {
        mainS = true;
        health = gameObject.GetComponent<HoldingVar>().health;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameInfo.instance.Paused)
        {
            if (mainS)
            {
                timer += Time.deltaTime;
            }
        }
        if (gOver)
        {
            timer = 0;
        }
        
            
       




    }
}
