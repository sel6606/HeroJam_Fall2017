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
    /// Game Over screen
    /// </summary>
    public void GameOver()
    {
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

