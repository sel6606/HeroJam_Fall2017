using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HoldingVar : MonoBehaviour {

    public float health;
    float maxHealth;
    public RawImage bar;
    float barWidth;

	// Use this for initialization
	void Start ()
    {
        maxHealth = health;
        barWidth = bar.rectTransform.sizeDelta.x;

        HealthBar();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (health == 0 )
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Damage()
    {
        health -= Time.deltaTime;
        //Debug.Log("health" + health);
       
        health = Mathf.Clamp(health, 0f, 1000f);
        //Debug.Log("max health" + maxHealth);

        HealthBar();
        
    }

    public void HealthBar()
    {
        float percentHealth = health / maxHealth;
        //Debug.Log("percentHealth" + percentHealth);
       
        float healthLeft = barWidth * percentHealth;
        //Debug.Log("barWidth" + barWidth);
        RectTransform innerBar = bar.GetComponent<RectTransform>();
        //Debug.Log("inner Bar size Delta" + innerBar.sizeDelta.x);
        innerBar.sizeDelta = new Vector2(healthLeft, bar.rectTransform.sizeDelta.y);
        
        innerBar.anchoredPosition = new Vector2(healthLeft/2 +6 , innerBar.anchoredPosition.y);
        


    }
}
