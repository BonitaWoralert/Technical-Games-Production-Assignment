using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vision_Script : MonoBehaviour
{
    [SerializeField] private Enemy_AI enemyAIScript;
    [SerializeField] private AIState lastAIState;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Makes sure that AIState is not NONE, this is done because OnTriggerEnter2D can be triggered multiple times.
            if(enemyAIScript.GetLastAIState() != AIState.NONE)
            {
                lastAIState = enemyAIScript.GetLastAIState();
            }
            Debug.Log(lastAIState);
            enemyAIScript.ChangeAIState(AIState.NONE);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyAIScript.ChangeAIState(lastAIState);
        }
    }
}
