using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Modifier", menuName = "Modifier")]
public class Modifier : ScriptableObject
{
    new public string name = "New Modifier";
   
    public virtual void Use()
    {
        // Do something

        Debug.Log("Using " + name);
    }
}
