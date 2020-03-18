using UnityEngine;
using System.Collections;

public class RayViewer : MonoBehaviour
{

    public float weaponRange = 50f;                       

    private Camera fpsCam;                                


    void Start()
    {
        
        fpsCam = GetComponentInParent<Camera>();
    }


    void Update()
    {
        
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0f, 0f, 0.0f));
    }
}