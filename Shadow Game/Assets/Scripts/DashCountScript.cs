using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashCountScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dashText;
    [SerializeField] private int currentDashCount;
    [SerializeField] private Movement movementScript;

    private void Start()
    {
        currentDashCount = movementScript.dashAmount;
    }
    // Update is called once per frame
    void Update()
    {
        currentDashCount = movementScript.dashAmount;
        dashText.text = "Dash: " + currentDashCount.ToString();
    }
}
