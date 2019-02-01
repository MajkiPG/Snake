using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverWindowController : MonoBehaviour
{
    public Text score;      //Stores text with last game score.

	// <summary>
    // Initializes game over window.
    // Sets score text.
    // </summary>
	private void Start ()
    {
        score.text = PlayerPrefs.GetInt("LastScore").ToString();
	}
	
    // <summary>
    // Replay button action.
    // Loads game scene.
    // </summary>
    public void ReplayButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    // <summary>
    // Quit button action.
    // Loads main menu scene.
    // </summary>
    public void QuitButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
