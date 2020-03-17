using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomController : MonoBehaviour
{

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(waiter());
    }

    public void Move(){
        int minimum = -20;
        int maximum = 20;
        float moveX = Random.Range(minimum, maximum);
        float moveZ = Random.Range(minimum, maximum);

        Vector3 newPosition = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z + moveZ);
        agent.SetDestination(newPosition);
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        int wait_time = Random.Range(0, 10);
        yield return new WaitForSeconds(wait_time);
        Move();
    }
}
