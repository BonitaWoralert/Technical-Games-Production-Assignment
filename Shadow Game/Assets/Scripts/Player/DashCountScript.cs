using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashCountScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dashText;
    [SerializeField] private int currentDashCount;
    [SerializeField] private PlayerStats statsScript;

    private void Start()
    {
        statsScript = FindObjectOfType<PlayerStats>();
        currentDashCount = statsScript.dashAmount;
    }
    // Update is called once per frame
    void Update()
    {
        currentDashCount = statsScript.dashAmount;
        dashText.text = "dash: " + currentDashCount.ToString();
    }
}