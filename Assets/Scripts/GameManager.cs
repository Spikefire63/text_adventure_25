using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<string> inventory = new List<string>();

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
        InputManager.instance.onRestart += ResetGame; // ResetGame() will be the code to respond to event
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream afile = File.Open(Application.persistentDataPath + "/Player.save", FileMode.Open);
            SaveState playerData = (SaveState)bf.Deserialize(afile);
            afile.Close();

            Room room = NavManager.instance.GetRoomFromName(playerData.currentRoom);
            if (room != null)
            {
                NavManager.instance.SwitchRoom(room);
            }

            inventory = playerData.inventory;

        }
        else
        {
            NavManager.instance.ResetGame();
        }
    }

    void ResetGame()
    {
        inventory.Clear();
    }

    public void Save()
    {
        //set up data to save
        SaveState playerState = new SaveState();
        playerState.currentRoom = NavManager.instance.currentRoom.name;
        playerState.inventory = inventory;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream afile = File.Create(Application.persistentDataPath + "/player.save");
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(afile, playerState);
        afile.Close();
    }
}