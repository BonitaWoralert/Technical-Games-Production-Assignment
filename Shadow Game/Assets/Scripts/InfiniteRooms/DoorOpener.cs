using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public enum DoorType
    {
        Up,
        Down,
        Left,
        Right
    }
    public DoorType doorType;

    public GameObject roomPlacement;
}
