using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawn : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        if (PlayerPrefs.GetInt("Spawn") == 1)
        {
            //Set the player position the the end of the level
            transform.position = new Vector3(43, 0.84f, 0);
            //Reset player pref
            PlayerPrefs.SetInt("Spawn", 0);
            PlayerPrefs.Save();
        }
    }
    public void LevelEntry()
    {

    }
}
