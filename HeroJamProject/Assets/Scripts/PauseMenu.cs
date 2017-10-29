using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that contains methods for pausing and unpausing the game
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;


    /// <summary>
    /// When the scene is finished, set the time scale back to normal
    /// </summary>
    private void OnDestroy()
    {
        GameInfo.instance.Paused = false;
    }

    // Use this for initialization
    void Start ()
    {
        pauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameInfo.instance.Paused)
            {
                PauseGame();
            }
            else
            {
                Unpause();
            }
        }
	}

    /// <summary>
    /// Pause the game and open the pause menu
    /// </summary>
    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        GameInfo.instance.Paused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Unpause the game and close the pause menu
    /// </summary>
    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        GameInfo.instance.Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
