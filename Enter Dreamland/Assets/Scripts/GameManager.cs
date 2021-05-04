/*
 *  Created by Nohl Egan
 *  Feb 23, 2021
 *  This script implements the GameManager, which will manage different aspects of the game such as life count, game overs, and victories.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerLives = 3;     // represents the number of lives the player has left
    public LifeCounter[] lifeCounters = new LifeCounter[3];     // each element of the array holds one of the LifeCounter GameObjects on the screen

    private bool paused = false;    // represents if the game is currently paused
    private bool gameOver = false;  // represents if the game is currently over due to the player dying
    private bool combatWon = false; // represents if the combat has been won

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);  // prevents the game manager from being deleted when the next challenge is loaded
        instance = this;    // Creates an instance of this GameManager object that will be used until either victory is achieved or the player dies
    }

    // Update is called once per frame
    private void Update()
    {
        // if the player hits escape, the game will pause/unpause
        if (Input.GetButtonDown("Cancel"))
        {
            paused = !paused;
            if (paused)
            {
                Time.timeScale = 0f;
                UI.instance.DisplayPauseScreen(true);
            }
            else
            {
                Time.timeScale = 1f;
                UI.instance.DisplayPauseScreen(false);
            }
        }
    }

    // called when the player reaches the goal in a challenge to check where the game should go next
    public void GoalReached()
    {   
        // if there are still more challenges to complete
        if (SceneManager.sceneCountInBuildSettings != SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        }
        // if there are no more challenges to complete
        else
        {
            combatWon = true;
            StartCoroutine(CombatWin());
        }
    }
    
    // called when the player is hit by an obstacle
    public void PlayerHit()
    {
        playerLives -= 1;
        lifeCounters[playerLives].Hide();
        
        // if the player runs out of lives (Game Over)
        if (playerLives == 0)
        {
            gameOver = true;
        }

        StartCoroutine(UI.instance.DisplayHitScreen());
    }

    // returns true if the game is over
    public bool IsGameOver()
    {
        return gameOver;
    }

    // returns true if the combat has been won
    public bool IsCombatWon()
    {
        return combatWon;
    }

    // this function will perform a number of actions in the event that the game is won
    private IEnumerator CombatWin()
    {
        StartCoroutine(UI.instance.DisplayWhiteScreen());
        yield return new WaitForSeconds(1f);

        UI.instance.DisplayVictoryScreen(true);
        SpriteRenderer sprite = UI.instance.victoryScreen.GetComponent<SpriteRenderer>();
        // fades in the victory screen
        for (float i = 0f; i <= 1.05f; i += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            sprite.color = new Color(255, 255, 255, i);
        }

        yield return new WaitForSeconds(3f);

        // Destroy each LifeCounter object
        for (int i = 0; i < lifeCounters.Length; i++)
        {
            lifeCounters[i].Remove();
        }

        // Destroy this instance of the GameManager
        Destroy(gameObject);

        SceneManager.LoadScene("StartMenu");
    }

    // this function will perform a number of actions in the event of a game over
    public IEnumerator GameOver()
    {
        UI.instance.DisplayGameOverScreen(true);
        
        // fades in the game over screen
        for (float i = 0f; i <= 1.03f; i += 0.03f)
        {
            UI.instance.gameOverScreenCanvas.alpha = i;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2f);

        // Destroy each LifeCounter object
        for (int i = 0; i < lifeCounters.Length; i++)
        {
            lifeCounters[i].Remove();
        }

        // Destroy this instance of the GameManager
        Destroy(gameObject);
        
        SceneManager.LoadScene("StartMenu");
    }
}
