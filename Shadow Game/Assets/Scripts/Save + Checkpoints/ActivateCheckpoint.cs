using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCheckpoint : MonoBehaviour
{
    public bool activated;
    public int index;
    private CheckpointManager checkpointManager;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        checkpointManager = FindObjectOfType<CheckpointManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision) //player collides with checkpoint
    {
        if(!activated)
        {
            checkpointManager.DeactivateAll(); //all other checkpoints off
            activated = true; //this checkpoint on
            Debug.Log("Checkpoint activated: " + index);
        }
    }
}
