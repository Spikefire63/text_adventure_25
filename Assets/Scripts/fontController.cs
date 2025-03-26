using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fontController : MonoBehaviour
{
    public Text displayText;
    public Text placeHolderText; // part of the input field for initial placeholder text
    public Text playerText;
    public Slider slider;

    private float fontSize;
   
    void Start()
    {
        fontSize = PlayerPrefs.GetFloat("fontSize", 14f);

        slider.minValue = 14f;
        slider.maxValue = 28f;

        slider.onValueChanged.AddListener(UpdateFontSize);
        UpdateFontSize(fontSize);
    }

    void UpdateFontSize(float value)
    {
        fontSize = value;
        PlayerPrefs.SetFloat("fontSize", fontSize); // Save the font size to PlayerPrefs

        // Apply the new font size to all text components
        displayText.fontSize = (int)value;
        placeHolderText.fontSize = (int)value;
        playerText.fontSize = (int)value;
    }
}
