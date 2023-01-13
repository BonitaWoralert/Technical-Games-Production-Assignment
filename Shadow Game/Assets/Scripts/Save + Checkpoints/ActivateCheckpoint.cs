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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!activated)
        {
            checkpointManager.DeactivateAll();
            activated = true;
            Debug.Log("Checkpoint activated: " + index);
        }
    }
}
