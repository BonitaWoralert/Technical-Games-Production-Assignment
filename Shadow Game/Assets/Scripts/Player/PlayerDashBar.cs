using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashBar : MonoBehaviour
{
    public Movement movement;
    public PlayerStats stats;
    public Image fillImage;
    private Slider slider;
    [SerializeField] private GameObject fill;

    private void Start()
    {
        movement = FindObjectOfType<Movement>();
        stats = FindObjectOfType<PlayerStats>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = movement.currentDashPower - stats.currentDashLevel;
    }
}
