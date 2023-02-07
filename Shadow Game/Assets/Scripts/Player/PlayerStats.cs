using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public float shadowEnergy;
    public int maxShadowEnergy = 1;
    private ShadowForm shadowForm;
    public float shadowDecreaseSpeed = 0.2f;
    public float shadowIncreaseSpeed = 0.4f;
    public int currentDashLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shadowForm = GetComponent<ShadowForm>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeShadowEnergy(0);
        AdminCommands();

    }

    private void AdminCommands()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeShadowEnergy(5f);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeShadowEnergy(100f);
        }
    }

    private void ChangeShadowEnergy(float change)
    {
        if (shadowForm.isInShadowForm)
        {
            shadowEnergy -= Time.deltaTime * shadowDecreaseSpeed;
        }
        if(!shadowForm.isInShadowForm && shadowEnergy < maxShadowEnergy)
        {
            shadowEnergy += Time.deltaTime * shadowIncreaseSpeed;
        }
        shadowEnergy += change;
    }
}