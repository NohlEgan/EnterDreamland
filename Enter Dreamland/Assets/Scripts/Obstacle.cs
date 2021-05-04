/*
 *  Created by Nohl Egan
 *  Feb 23, 2021
 *  This script implements the movement for the obstacle
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector2[] movementPattern;   // this is the movement pattern which the obstacle will follow in an endless cycle
    public float movementSpeed;

    private int destinationPoint = 0;   // represents which point in the movement pattern the obstacle will move to next

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        transform.position = movementPattern[0];
    }

    // Update is called once per frame
    private void Update()
    {
        // prevents the obstacle from moving if the combat is won or the game is over
        if (GameManager.instance.IsCombatWon() || GameManager.instance.IsGameOver())
        {
            return;
        }

        Vector2 currentDest = movementPattern[destinationPoint];

        // if the obstacle has reached destinationPoint, update destinationPoint
        if (Vector2.Distance(transform.position, currentDest) == 0)
        {
            destinationPoint = (destinationPoint + 1) % movementPattern.Length;
            currentDest = movementPattern[destinationPoint];
        }

        transform.position = Vector2.MoveTowards(transform.position, currentDest, movementSpeed * Time.deltaTime);  // moves the obstacle towards destinationPoint
    }
}
