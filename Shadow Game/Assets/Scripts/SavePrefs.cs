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

    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("CheckpointFlag", checkpointFlag);
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
            health = PlayerPrefs.GetInt("Health");
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
