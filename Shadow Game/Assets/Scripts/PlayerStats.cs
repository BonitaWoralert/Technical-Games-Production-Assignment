using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public float shadowEnergy;

    private ShadowForm shadowForm;

    // Start is called before the first frame update
    void Start()
    {
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
            shadowEnergy -= Time.deltaTime;
        }
        shadowEnergy += change;
    }
}
