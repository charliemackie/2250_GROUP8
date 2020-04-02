using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExperience : MonoBehaviour
{
    // xp value
    public int xp;

    // steal experience method that should be different for various characters
    public virtual void stealExp(CharacterExperience other)
    {
        this.xp = this.xp + other.xp;
        other.xp = 0;

        StairScript.instance.spawnSteps();
    }
}
