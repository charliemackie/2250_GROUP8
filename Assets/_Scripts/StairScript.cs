﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    #region Singleton

    public static StairScript instance;

    private void Awake()
    {
        instance = new StairScript();
        instance = this;
    }

    #endregion

    // Get the different stairs
    public GameObject groundStairs;
    public GameObject middleStairs;
    public GameObject gate;

    // Hide the stairs at the beginning of the game
    private void Start()
    {
        hideStairs();
    }

    // Unlock the steps depending on the player's xp
    public void spawnSteps()
    {
        if (PlayerStats.instance.GetComponent<CharacterExperience>().xp >= 265)
        {
            groundStairs.SetActive(true);
        }

        if (PlayerStats.instance.GetComponent<CharacterExperience>().xp >= 390)
        {
            middleStairs.SetActive(true);
            gate.SetActive(true);

        }
    }

    public void hideStairs()
    {
        groundStairs.SetActive(false);
        middleStairs.SetActive(false);
        gate.SetActive(false);
    }
}
