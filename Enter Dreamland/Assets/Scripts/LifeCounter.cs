/*
 *  Created by Nohl Egan
 *  Feb 23, 2021
 *  This script implements the functions for the life counter which allows each life count to be hidden or removed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    private SpriteRenderer sprite;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);  // prevents this gameObject from being deleted when the next challenge is loaded
        sprite = GetComponent<SpriteRenderer>();
    }

    // makes this gameObject invisible
    public void Hide()
    {
        sprite.color = new Color(0, 0, 0, 0);
    }

    // destroys this gameObject
    public void Remove()
    {
        Destroy(gameObject);
    }
}
