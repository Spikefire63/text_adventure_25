using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text/Exit")]
public class Exit : ScriptableObject
{
    //directions a player can go
    public enum Direction { north, south, east, west }

    public Direction direction;
    [TextArea]
    public string description;
    public Room room;

    //public variables to show if a room is hidden or locked
    public bool isLocked;
    public bool isHidden;
}
