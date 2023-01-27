using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public float shadowEnergy;
    public int shadowLevel;
    public int maxShadowLevel;

    private ShadowForm shadowForm;

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

        shadowLevel = Mathf.FloorToInt(shadowEnergy);
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
            shadowEnergy -= Time.deltaTime;
        }
        shadowEnergy += change;
    }
}
