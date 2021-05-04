/*
 *  Created by Nohl Egan
 *  Feb 23, 2021
 *  This script implements the screen overlay displays such as the pause screen, game over screen, and the hit screen
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject hitScreen;
    public GameObject whiteScreen;
    public GameObject victoryScreen;
    public CanvasGroup gameOverScreenCanvas;
    public static UI instance;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        instance = this;
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        hitScreen.SetActive(false);
        whiteScreen.SetActive(false);
        victoryScreen.SetActive(false);
        gameOverScreenCanvas = gameOverScreen.GetComponent<CanvasGroup>();
        gameOverScreenCanvas.alpha = 0;
    }

    // this function displays the pause screen
    public void DisplayPauseScreen(bool display)
    {
        pauseScreen.SetActive(display);
    }

    // this function sets the game over screen to active so that it can be seen fading in
    public void DisplayGameOverScreen(bool display)
    {
        gameOverScreen.SetActive(display);
    }

    // this function sets the victory screen to active so that it can be seen fading in
    public void DisplayVictoryScreen(bool display)
    {
        victoryScreen.SetActive(display);
    }

    // this function displays the white screen on victory for a very short period of time
    public IEnumerator DisplayWhiteScreen()
    {
        whiteScreen.SetActive(true);
        yield return new WaitForSeconds(0.12f);
        whiteScreen.SetActive(false);
    }

    // this function displays the hit screen for a very short period of time
    public IEnumerator DisplayHitScreen()
    {
        hitScreen.SetActive(true);
        yield return new WaitForSeconds(0.12f);
        hitScreen.SetActive(false);
    }
}
