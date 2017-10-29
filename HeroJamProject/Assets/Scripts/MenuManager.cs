using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// This script is attached to the menu manager to handle all menu handling
/// </summary>
public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Start The game
    /// </summary>
    public void StartGame()
    {
        if (GameInfo.instance != null)
        {
            GameInfo.instance.gameObject.GetComponent<Timer>().mainS = true;
            GameInfo.instance.gameObject.GetComponent<Timer>().gOver = false;
            GameInfo.instance.gameObject.GetComponent<Timer>().levelCom = false;
        }
       
        SceneManager.LoadScene("MainScene");

    }

    /// <summary>
    /// Load Instuctions
    /// </summary>
    public void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    /// <summary>
    /// Load Main Menu
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Load Credits
    /// 
    /// </summary>
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    /// <summary>
    /// Game Over screen
    /// </summary>
    public void GameOver()
    {
        GameInfo.instance.gameObject.GetComponent<Timer>().mainS = false;
        GameInfo.instance.gameObject.GetComponent<Timer>().gOver = true;
        GameInfo.instance.gameObject.GetComponent<Timer>().levelCom = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOver");
    }


    /// <summary>
    /// Quit the Game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}

