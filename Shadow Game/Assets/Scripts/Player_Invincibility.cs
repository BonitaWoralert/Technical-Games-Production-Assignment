using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Invincibility : MonoBehaviour
{
    [SerializeField] private float invincibilityTime;
    [SerializeField] public float maxInvincibilityTime;
    [SerializeField] private bool isInvincible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInvincible == true)
        {
            InvicibilityTimeUpdate();
        }
    }

    private void InvicibilityTimeUpdate()
    {
        invincibilityTime -= Time.deltaTime;
        if(invincibilityTime <= 0)
        {
            //Invincibility is over.
            invincibilityTime = maxInvincibilityTime;
            isInvincible = false;
        }
        else
        {
            //Player is Invincible.
            isInvincible = true;
        }
    }

    public void SetInvincibility(bool newState)
    {
        isInvincible = newState;
    }

    public bool GetInvincibility()
    {
        return isInvincible;
    }
    
}
