using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : Collectible
{
    // == Private Fields ==
    private int lives;

    protected override void AddEffect()
    {
        print("Extra Life Collected!");

        // Get lives from player prefs
        lives = PlayerPrefs.GetInt("Lives");

        // If between 0 and 3
        if (lives >= 0 && lives < 3)
        {
            // Add life
            lives++;

            // Set new life
            PlayerPrefs.SetInt("Lives", lives);
        }
    }
}
