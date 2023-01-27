using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShadowBar : MonoBehaviour
{
    public PlayerStats playerStats;
    public Image fillImage;
    private Slider slider;
    [SerializeField] private GameObject fill; 

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Hp is Zero.
        if(slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }

        //Hp is still available.
        if(slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        //Player Health is in Critical condition.
        //if(slider.value == 1)
        //{
        //    ChangeHealthBarColor(Color.yellow);
        //}
        //else
        //{
        //    ChangeHealthBarColor(Color.red);
        //}
        float fillValue = (float)playerStats.shadowEnergy;
        slider.value = fillValue;
    }

    public void ChangeHealthBarColor(Color newColor)
    {
        fillImage.color = newColor;
    }
}
