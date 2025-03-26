using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button helpButton; // The Help button
    public Text helpText; // The text that displays the list of commands
    
    void Start()
    {
        helpButton.onClick.AddListener(OnHelpButtonClicked);
    }

    void OnHelpButtonClicked()
    {
        InputManager.instance.UpdateStory("Use the command 'commands' to see the list of available commands.");
    }
}
