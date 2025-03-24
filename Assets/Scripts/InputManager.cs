using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Text storyText; // the story 
    public InputField userInput; // the input field object
    public Text inputText; // part of the input field where user enters response
    public Text placeHolderText; // part of the input field for initial placeholder text

    public delegate void Restart();
    public event Restart onRestart;
    //public InputField abutton;
    private string story; // holds the story to display
    private List<string> commands = new List<string>();// valid user commands

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        commands.Add("restart");
        commands.Add("save");
        commands.Add("go");
        commands.Add("get");
        commands.Add("grab");
        //commands.Remove("use");

        userInput.onEndEdit.AddListener(GetInput);
     //   abutton.onClick.AddListener(DoSomething);
        story = storyText.text;
        NavManager.instance.onGameOver += EndGame; // funtion to call when event occurs
    }

    void EndGame()
    {
        UpdateStory("\nPlease enter 'restart' to play again.");
    }
    //void DoSOmething()
    //{
    //    Debug.Log("Button clicked!");
    //}
    
    public void UpdateStory(string msg)
    {
        story = "\n" + msg;
        storyText.text = story;
    }

    void GetInput(string msg)
    {
        if (msg != "")
        {
            char[] splitInfo = { ' ' };
            string[] parts = msg.ToLower().Split(splitInfo);// ["go", "north"]

            if (commands.Contains(parts[0])) //if valid command
            {
                if (parts[0] == "go")
                {
                    if (NavManager.instance.SwitchRoom(parts[1])) // returns true if direction exits
                    {
                        //fill in later
                    }
                    else
                    {
                        UpdateStory("Exit does not exist or is locked. Try again.");
                    }
                }
                else if (parts[0] == "grab")
                {
                    if (NavManager.instance.TakeItem(parts[1]))
                    {
                        GameManager.instance.inventory.Add(parts[1]);
                        UpdateStory("You added a(n) " + parts[1] + " to your inventory.");
                    }
                    else
                    {
                        UpdateStory("Sorry, " + parts[1] + " does not exist in this room.");
                    }
                }
                else if (parts[0] == "restart")
                {
                    if (onRestart != null) // if anyone is listening
                        onRestart(); // invoke the event
                }
                else if (parts[0] == "save")
                {
                    GameManager.instance.Save();
                }    

                //else if (parts[0] == "use")
                //{
                //    if (NavManager.instance.UseItem(parts[1]))
                //    {
                //        GameManager.instance.inventory.Remove(parts[1]);
                //        UpdateStory("You opened the door");
                //    }
                //}
            }
        }
        userInput.text = "";
        userInput.ActivateInputField();

    }
}

