using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Rotating the pickup objects non stop in the x, y, and z as a function of time
        transform.Rotate (new Vector3(15, 30, 45)*Time.deltaTime);
    }
}
