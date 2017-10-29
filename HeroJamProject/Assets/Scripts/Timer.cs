using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    public float timer;
    public float score;
    public float health;

    bool level;
	// Use this for initialization
	void Start ()
    {
        health = gameObject.GetComponent<HoldingVar>().health;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainScene"))
        {
            timer += Time.deltaTime;
        }

       
    }
}
