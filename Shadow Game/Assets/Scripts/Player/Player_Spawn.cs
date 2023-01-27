using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawn : MonoBehaviour
{
    private Vector3 objpos;

    void Start()
    {
        if (PlayerPrefs.GetInt("Spawn") == 1)
        {
            //Set the player position to the end of the level
            objpos = new Vector3(GameObject.Find("Next Level Trigger").transform.position.x - 2, 
                GameObject.Find("Next Level Trigger").transform.position.y, GameObject.Find("Next Level Trigger").transform.position.z);
            transform.position = objpos;

            //Sets the rotation of the player
            if (PlayerPrefs.GetInt("SpawnFlip") == 1)
                GetComponent<SpriteRenderer>().flipX = true;
            else
                GetComponent<SpriteRenderer>().flipX = false;

            //Reset player pref
            PlayerPrefs.SetInt("Spawn", 0);
            PlayerPrefs.SetInt("SpawnFlip", 0);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.GetInt("Spawn") == 2)
        {
            //Set the player position to the end of the level
            objpos = new Vector3(GameObject.Find("Previous Level Trigger").transform.position.x + 2 ,
                GameObject.Find("Previous Level Trigger").transform.position.y, GameObject.Find("Previous Level Trigger").transform.position.z);
            transform.position = objpos;

            //Sets the rotation of the player
            if (PlayerPrefs.GetInt("SpawnFlip") == 1)
                GetComponent<SpriteRenderer>().flipX = true;
            else
                GetComponent<SpriteRenderer>().flipX = false;

            //Reset player pref
            PlayerPrefs.SetInt("Spawn", 0);
            PlayerPrefs.SetInt("SpawnFlip", 0);
            PlayerPrefs.Save();
        }
    }
}