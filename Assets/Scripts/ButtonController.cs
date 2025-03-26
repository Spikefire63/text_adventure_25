using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button helpButton; //the button
    public Text helpText; // desplay the help text
    
    void Start()
    {
        helpButton.onClick.AddListener(OnHelpButtonClicked);
    }

    void OnHelpButtonClicked()
    {
        InputManager.instance.UpdateStory("Use the command 'commands' to see the list of available commands.");
    }
}
