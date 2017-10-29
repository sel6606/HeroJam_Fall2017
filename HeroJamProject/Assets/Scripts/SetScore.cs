using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    public Text score;
	// Use this for initialization
	void Start ()
    {
		score.text = "Time Completed: " + GameInfo.instance.gameObject.GetComponent<Timer>().timer;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
