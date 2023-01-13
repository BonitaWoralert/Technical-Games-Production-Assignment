using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{
    //all variables to be saved
    int checkpointFlag = 0;
    int health;
    int collectible;
    int score;
    float timer;

    //all references containing data that needs to be saved
    private PlayerStats stats;
    private CheckpointManager checkpointManager;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //initialise references
        player = GameObject.FindGameObjectWithTag("Player");
        stats = FindObjectOfType<PlayerStats>();
        checkpointManager = FindObjectOfType<CheckpointManager>();

        LoadGame(); //Load existing save when game starts
    }

    public void SaveGame() //files are saved in Registry Editor: Computer\HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\DefaultCompany\Shadow Game
    {
        //save index of current checkpoint in the list of checkpoints
        //player location
        checkpointFlag = checkpointManager.GetCurrentCheckpoint();
        PlayerPrefs.SetInt("CheckpointFlag", checkpointFlag); 

        //save int health
        health = stats.health;
        PlayerPrefs.SetInt("Health", health); 

        PlayerPrefs.SetInt("Collectible", collectible);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetFloat("Timer", timer);
        PlayerPrefs.Save();
        Debug.Log("Game Data Saved");
    }

    public void LoadGame() 
    {
        if (PlayerPrefs.HasKey("CheckpointFlag")) //making sure playerprefs isn't empty
        {
            checkpointFlag = PlayerPrefs.GetInt("CheckpointFlag"); //set checkpointflag variable to data inside save file
            checkpointManager.SetCurrentCheckpoint(checkpointFlag); //set current checkpoint
            player.transform.position = checkpointManager.GetCheckpointPos(); //set player position

            health = PlayerPrefs.GetInt("Health"); //set health var to health in file
            stats.health = health; //set health in player stats

            collectible = PlayerPrefs.GetInt("Collectible");
            score = PlayerPrefs.GetInt("Score");
            timer = PlayerPrefs.GetFloat("Timer");
            Debug.Log("Game Data Loaded");
        }
        else
            Debug.LogError("No save data");
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll(); //delete all data in playerprefs
        //reset all variables to 0
        checkpointFlag = 0;
        health = 0;
        collectible = 0;
        score = 0;
        timer = 0.0f;
        Debug.Log("Game Data Reset");
    }
}
