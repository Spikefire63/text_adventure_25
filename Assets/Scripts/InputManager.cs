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
    public Text placeHolderText; // part of the input field for initial placeholder tex

    public delegate void Restart();
    public event Restart onRestart;

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
        //list of all available commands
        commands.Add("restart");
        commands.Add("save");
        commands.Add("go");
        commands.Add("grab");
        commands.Add("commands");
        commands.Add("inventory");
        //commands.Remove("use");

        userInput.onEndEdit.AddListener(GetInput);
     //   abutton.onClick.AddListener(DoSomething);
        story = storyText.text;
        NavManager.instance.onGameOver += EndGame; // funtion to call when event occurs
    }

    //end game function
    void EndGame()
    {
        UpdateStory("\nPlease enter 'restart' to play again.");
    }
    
    //updates the story with a double space for clear view of text
    public void UpdateStory(string msg)
    {
        story += "\n \n" + msg;
        storyText.text = story;
    }

    //list of the commands fuctions
    void GetInput(string msg)
    {
        if (msg != "")
        {
            char[] splitInfo = { ' ' };
            string[] parts = msg.ToLower().Split(splitInfo);// ["go", "north"]

            if (commands.Contains(parts[0])) //if valid command
            {
                //command function for "go" direction
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

                //command function for "go" direction
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

                //command function for "restart" for when the player falls into a trap
                else if (parts[0] == "restart")
                {
                    if (onRestart != null) // if anyone is listening
                        onRestart(); // invoke the event
                }

                //command function for "save" for saving the gamestate
                else if (parts[0] == "save")
                {
                    GameManager.instance.Save();
                }

                //command function for "commands" that shows the full list of commands to the player
                else if (parts[0] == "commands")
                {
                    UpdateStory(string.Join(", ", commands));
                }

                //command function for "inventory" to view your inventory in game
                else if (parts[0] == "inventory")
                {
                    msg = "You have a(n) ";

                    if (GameManager.instance.inventory.Count == 0)
                    {
                        msg = "You have nothing in your inventory.";
                    }
                    else
                    {
                        foreach (string item in GameManager.instance.inventory)
                        {
                            msg += item + ", ";
                        }

                        msg = msg.TrimEnd(',', ' ');
                    }
                    // Update the story with the final message
                    UpdateStory(msg);
                }

                //This is a function i want to eventually implement correctly where the player
                //has to use the items the collect around the map to access new locations.

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

