using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    //activated checkpoint
    public bool activated = false; 

    //list of all checkpoints in scene
    public List<GameObject> CheckPointsList;
    GameObject activeCheckPoint;
    int setindex;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        //look for all checkpoints
        foreach (var i in FindObjectsOfType<ActivateCheckpoint>())
        {
            CheckPointsList.Add(i.gameObject);
            i.GetComponent<ActivateCheckpoint>().index = setindex;
            setindex++;
        }
    }

    internal void DeactivateAll() //deactivate all checkpoints before activating current one
    {
        foreach (GameObject checkPoint in CheckPointsList)
        {
            checkPoint.GetComponent<ActivateCheckpoint>().activated = false;
        }
    }

    public int GetCurrentCheckpoint() //returns current checkpoint, for saving or for returning to checkpoint
    {
        foreach (GameObject checkPoint in CheckPointsList)
        {
            if(checkPoint.GetComponent<ActivateCheckpoint>().activated == true)
            {
                index = checkPoint.GetComponent<ActivateCheckpoint>().index;
            }
        }
        Debug.Log("Got index: " + index);
        return index;
    }

    public void SetCurrentCheckpoint(int newindex) //set new current checkpoint, for loading a save
    {
        foreach (GameObject checkPoint in CheckPointsList)
        {
            checkPoint.GetComponent<ActivateCheckpoint>().activated = false;
        }
        CheckPointsList[newindex].GetComponent<ActivateCheckpoint>().activated = true; //activate correct checkpoint
        Debug.Log("Current index set to: " + index);
    }

    public Vector2 GetCheckpointPos()
    {
        return CheckPointsList[GetCurrentCheckpoint()].GetComponent<ActivateCheckpoint>().transform.position;
    }
}
