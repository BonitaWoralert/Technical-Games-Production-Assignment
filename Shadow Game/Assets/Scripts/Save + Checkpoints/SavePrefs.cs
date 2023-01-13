using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{
    public int checkpointFlag;
    public int health;
    public int collectible;
    public int score;
    public float timer;

    private PlayerStats stats;
    private CheckpointManager checkpointManager;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LoadGame(); //Load save when game starts
        stats = FindObjectOfType<PlayerStats>();
        checkpointManager = FindObjectOfType<CheckpointManager>();
        health = stats.health;
        Debug.Log("Loaded health is" + stats.health);
    }

    public void SaveGame() //files are saved in Registry Editor: Computer\HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\DefaultCompany\Shadow Game
    {
        checkpointFlag = checkpointManager.GetCurrentCheckpoint();
        PlayerPrefs.SetInt("CheckpointFlag", checkpointFlag); //location on map

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
        if (PlayerPrefs.HasKey("CheckpointFlag"))
        {
            checkpointFlag = PlayerPrefs.GetInt("CheckpointFlag");
            checkpointManager.SetCurrentCheckpoint(checkpointFlag);
            player.transform.position = checkpointManager.GetCheckpointPos();

            health = PlayerPrefs.GetInt("Health");
            stats.health = health;

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
        PlayerPrefs.DeleteAll();
        checkpointFlag = 0;
        health = 0;
        collectible = 0;
        score = 0;
        timer = 0.0f;
        Debug.Log("Game Data Reset");
    }
}
