using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterScoreText : MonoBehaviour
{
    void Update()
    {
        RegisterScore();
    }

    // Registers the score to the text component
    public void RegisterScore()
    {
        // Display the Score from GameData on Screen
        GameData.singleton.scoreText = this.GetComponent<TextMeshProUGUI>();
    }
}
