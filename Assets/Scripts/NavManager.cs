using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavManager : MonoBehaviour
{

    public static NavManager instance;
    public Room startingRoom;
    public Room currentRoom;
    public List<Room> rooms;

    //new delegate
    public delegate void GameOver();
    public event GameOver onGameOver;

    public Exit toKeyNorth;

    private Dictionary<string, Room> exitRooms = new Dictionary<string, Room>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        InputManager.instance.onRestart += ResetGame;
        //Debug.Log(startingRoom.description);
        //toKeyNorth.isHidden = true;
        //currentRoom = startingRoom;
        //Unpack();
        ResetGame();
    }

    public void ResetGame()
    {
        toKeyNorth.isHidden = true;
        currentRoom = startingRoom;
        Unpack();
    }

    // Update is called once per frame
    void Unpack()
    {
        string description = currentRoom.description;
        exitRooms.Clear();

        foreach (Exit e in currentRoom.exits)
        {
            if (!e.isHidden)
            {
                description += " " + e.description;
                exitRooms.Add(e.direction.ToString(), e.room);
            }
        }
        InputManager.instance.UpdateStory(description);
        if(exitRooms.Count == 0)
            if(onGameOver != null)//is anyone listening
            onGameOver();//invoking the event

    }

    public bool SwitchRoom(string direction)
    {
        if (exitRooms.ContainsKey(direction))
        {
            if (!getExit(direction).isLocked || GameManager.instance.inventory.Contains("key"))
            {
                currentRoom = exitRooms[direction];
                InputManager.instance.UpdateStory("You go " + direction);

                Unpack();
                return true;
            }
        }

        return false;
    }

    public void SwitchRoom(Room room)
    {
        currentRoom = room;
        Unpack();
    }

    Exit getExit(string drirection)
    {
        foreach( Exit e in currentRoom.exits)
        {
            if (e.direction.ToString() == drirection)
                return e; // returns exit
        }
        return null;
    }

    public bool TakeItem(string item)
    {
        if (item == "key" && currentRoom.hasKey)
            return true;
        else if (item == "orb" && currentRoom.hasOrb)
        {
            toKeyNorth.isHidden = false;
            return true;
        }
        else
            return false;
    }

    public Room GetRoomFromName(string name)
    {
        foreach(Room aRoom in rooms)
        {
            if(aRoom.name == name)
                return aRoom;
        }
        return null;
    }

    //public bool UseItem(string item, string direction)
    //{
    //    if (GameManager.instance.inventory.Contains("key") && getExit(direction).isLocked)
     //   {

    //    }

    //}

}

