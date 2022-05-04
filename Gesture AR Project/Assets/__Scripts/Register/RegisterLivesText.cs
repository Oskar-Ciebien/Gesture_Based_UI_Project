using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterLivesText : MonoBehaviour
{
    void Update()
    {
        RegisterLives();
    }

    // Registers the lives to the text component
    public void RegisterLives()
    {
        // Display the Lives from GameData on Screen
        GameData.singleton.livesText = this.GetComponent<TextMeshProUGUI>();
    }
}
