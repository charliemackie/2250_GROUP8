using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    // Create a singleton for the StatsUI
    #region Singleton

    public static StatsUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of StatsUI found!");
            return;
        }
        instance = this;
    }

    #endregion

    // Text for each stat
    public Text speedCount;
    public Text strengthCount;
    public Text defenseCount;

    // Create instance of player
    PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {   
        // Awake the playerController instance
        player = PlayerStats.instance;

        // Update the stats
        UpdateStats();
    }

    // Update the stats with the new numbers
    public void UpdateStats()
    {
        // Set the stats to the appropriate character stats
        speedCount.text = player.speed.ToString();
        strengthCount.text = player.strength.ToString();
        defenseCount.text = player.defense.ToString();
    }
}
