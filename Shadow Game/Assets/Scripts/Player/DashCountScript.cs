using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashCountScript : MonoBehaviour
{
    [SerializeField] private PlayerStats statsScript;
    public GameObject[] dashes;

    private void Start()
    {
        statsScript = FindObjectOfType<PlayerStats>();
    }
    // Update is called once per frame
    void Update()
    {
        if (statsScript.dashAmount < 1)
        {
            foreach (GameObject dash in dashes)
            {
                dash.SetActive(false);
            }
        }
        else if (statsScript.dashAmount < 2)
        {
            foreach (GameObject dash in dashes)
            {
                dash.SetActive(false);
            }

            dashes[0].SetActive(true);
        }
        else if (statsScript.dashAmount < 3)
        {
            foreach (GameObject dash in dashes)
            {
                dash.SetActive(false);
            }

            dashes[0].SetActive(true);
            dashes[1].SetActive(true);
        }
        else if (statsScript.dashAmount < 4)
        {
            foreach (GameObject dash in dashes)
            {
                dash.SetActive(false);
            }

            dashes[0].SetActive(true);
            dashes[1].SetActive(true);
            dashes[2].SetActive(true);
        }
        else if (statsScript.dashAmount < 5)
        {
            foreach (GameObject dash in dashes)
            {
                dash.SetActive(false);
            }

            dashes[0].SetActive(true);
            dashes[1].SetActive(true);
            dashes[2].SetActive(true);
            dashes[3].SetActive(true);
        }
        else
        {
            foreach (GameObject dash in dashes)
            {
                dash.SetActive(true);
            }
        }
    }
}
