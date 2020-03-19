/*
 * This class models an enemy that has random movements
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomController : MonoBehaviour
{
    NavMeshAgent agent; // The mesh around the enemy

    /// <summary>
    /// Called at the beginning of creating the object
    /// </summary>
    void Start()
    {
        // Get the nav mesh and trigger the movement of the enemy
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(waiter());
    }

    /// <summary>
    /// How the enemy moves randomly
    /// </summary>
    public void Move(){

        // Finding random X and Z coordinates for the enemy to move to
        int minimum = -20;
        int maximum = 20;
        float moveX = Random.Range(minimum, maximum);
        float moveZ = Random.Range(minimum, maximum);

        // Setting the new position the enemy should move toward
        Vector3 newPosition = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z + moveZ);
        agent.SetDestination(newPosition);
        StartCoroutine(waiter());
    }

    /// <summary>
    /// Get a random wait time for the enemy to switch position
    /// </summary>
    /// <returns></returns>
    IEnumerator waiter()
    {
        int wait_time = Random.Range(0, 10);
        yield return new WaitForSeconds(wait_time);
        Move();
    }
}
