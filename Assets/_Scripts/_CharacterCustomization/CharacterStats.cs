using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // Set max health
    public int maxHealth = 100;

    // All character stats
    public int speed = 0;
    public int defense = 0;
    public int strength = 0;

    public virtual void Die()
    {
        // Die in some way
        Debug.Log(transform.name + " died.");
    }

}
