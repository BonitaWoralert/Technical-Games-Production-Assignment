using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawn : MonoBehaviour
{
    public GameObject player;
    private Vector3 objpos;

    void Start()
    {
        if (PlayerPrefs.GetInt("Spawn") == 1)
        {
            //Set the player position to the end of the level
            objpos = new Vector3(GameObject.Find("Next Level Trigger").transform.position.x - 2, 
                GameObject.Find("Next Level Trigger").transform.position.y, GameObject.Find("Next Level Trigger").transform.position.z);
            transform.position = objpos;
            //Reset player pref
            PlayerPrefs.SetInt("Spawn", 0);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.GetInt("Spawn") == 2)
        {
            //Set the player position to the start of the level
            objpos = new Vector3(GameObject.Find("Previous Level Trigger").transform.position.x + 20,
                GameObject.Find("Next Level Trigger").transform.position.y, GameObject.Find("Next Level Trigger").transform.position.z);
            transform.position = objpos;
            //Reset player pref
            PlayerPrefs.SetInt("Spawn", 0);
            PlayerPrefs.Save();
        }
    }
}