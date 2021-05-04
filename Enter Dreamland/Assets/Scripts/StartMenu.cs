/*
 *  Created by Nohl Egan
 *  Feb 23, 2021
 *  This script implements functions for the on-screen buttons, allowing the user to start or quit the game
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Loads the first challenge when the start button is pressed
    public void StartButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    // Exits the application when the quit button is pressed
    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
