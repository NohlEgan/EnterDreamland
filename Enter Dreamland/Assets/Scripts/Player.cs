/*
 *  Created by Nohl Egan
 *  Feb 23, 2021
 *  This script implements the functions used for the player movement, when the player gets hit, and the player death
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public Vector2 startPosition;

    private Rigidbody2D body;
    private bool hit = false;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true; // keeps the player from rotating
        transform.position = startPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        // the player can only move if they are not currently being hit by an obstacle and the game/combat is not currently over
        if (!hit && !GameManager.instance.IsGameOver() && !GameManager.instance.IsCombatWon())
        {
            Move();
        }
    }

    // Moves the player object in the direction according to the input
    private void Move()
    { 
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");

        body.velocity = new Vector2(xMovement, yMovement) * movementSpeed;  // moves the player object
    }
    
    // When the player is hit by an obstacle, this function will move them back to the start of the challenge
    private IEnumerator Hit()
    {
        hit = true;
        body.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.75f);
        body.position = startPosition;
        hit = false;
    }

    // This function is called when the player dies. It will make them fade away.
    private IEnumerator Death()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        body.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1f);
        
        // Makes the player sprite fade away
        for (float i = 1f; i >= 0f; i -= 0.01f)
        {
            yield return new WaitForSeconds(0.0075f);
            sprite.color = new Color(255, 255, 255, i);
        }
        
        StartCoroutine(GameManager.instance.GameOver());
    }

    // This function is called when the player enters an object with a trigger. In this case, that object would be an obstacle or the goal
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the player comes into contact with an obstacle
        if (other.CompareTag("Obstacle") && !hit && !GameManager.instance.IsGameOver())
        {
            GameManager.instance.PlayerHit();

            // checks if the game is over
            if (GameManager.instance.IsGameOver())
            {
                StartCoroutine(Death());
            }
            else
            {
                StartCoroutine(Hit());
            }
        }

        // if the player comes into contact with the goal
        else if (other.CompareTag("Goal"))
        {
            GameManager.instance.GoalReached();
            body.velocity = new Vector2(0, 0);
        }
    }
}
