using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("ResetSuperFood", 12f, 12f);
    }
    // <summary>
    // Play button action.
    // Loads game scene.
    // </summary>
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    // <summary>
    // Adds super food to menu instruction after dissaperance.
    // </summary>
    private void ResetSuperFood()
    {
        Instantiate(Globals.superFoodPrefab, new Vector2(-2.5f, -4.5f), Quaternion.identity);
    }
}
