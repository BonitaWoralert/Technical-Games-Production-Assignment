using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vision_Script : MonoBehaviour
{
    [SerializeField] private Enemy_AIAaron enemyAIScript;
    [SerializeField] private AIState2 lastAIState;
    [SerializeField] private Collider2D playerColliderBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision == playerColliderBox)
        {
            //Makes sure that AIState is not NONE, this is done because OnTriggerEnter2D can be triggered multiple times.
            if(enemyAIScript.GetLastAIState2() != AIState2.NONE)
            {
                lastAIState = enemyAIScript.GetLastAIState2();
            }
            enemyAIScript.isPlayerSpotted = true;
            Debug.Log("PLAYER SEEN");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision == playerColliderBox)
        {
            if(enemyAIScript.GetLastAIState2() == AIState2.CHASE)
            {
                enemyAIScript.ChangeAIState2(AIState2.SUSPICIOUS);
            }

            enemyAIScript.isPlayerSpotted = false;
            Debug.Log("PLAYER GONE");
        }
    }
}
