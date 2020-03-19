using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Create slider
    public Slider slider;

    // Determine the max value of slider
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Set the slider value to the updated value
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
