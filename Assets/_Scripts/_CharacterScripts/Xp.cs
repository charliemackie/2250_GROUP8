﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Xp : MonoBehaviour
{
    // Adjust the xpBar when the xp changes
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // Set the gradient and value of the xpBar
    public void SetXp(int xp)
    {
        slider.value = xp;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
