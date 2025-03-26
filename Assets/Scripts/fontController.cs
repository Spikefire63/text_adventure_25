using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fontController : MonoBehaviour
{
    public Text displayText;
    public Text placeHolderText;
    public Text playerText;
    public Slider slider;

    private float fontSize;
   
    void Start()
    {
        //set defult font
        fontSize = PlayerPrefs.GetFloat("fontSize", 14f);

        //created restraints for the font size
        slider.minValue = 14f;
        slider.maxValue = 28f;

        slider.onValueChanged.AddListener(UpdateFontSize);
        UpdateFontSize(fontSize);
    }

    void UpdateFontSize(float value)
    {
        fontSize = value;

        // Save the font size
        PlayerPrefs.SetFloat("fontSize", fontSize);

        // changes the size of all the text.
        displayText.fontSize = (int)value;
        placeHolderText.fontSize = (int)value;
        playerText.fontSize = (int)value;
    }
}
