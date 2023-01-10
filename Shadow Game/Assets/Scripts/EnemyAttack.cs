using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider2D attackBoxCollider;
    private GameObject playerObject;
    private BoxCollider2D playerBoxCollider;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        playerBoxCollider = playerObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision ==  playerBoxCollider)
        {
            Debug.Log("PLAYER TAKES DAMAGE");
        }
    }
}
