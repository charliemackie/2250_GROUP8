using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    public int currentHealth;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if(playerInRange && PlayerStats.attack == true){
            currentHealth -= (25 - defense);
            StartCoroutine(enemyHurt());
        }
    }

    override public void Die(){
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            print("IN RANGE");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            print("OUT OF RANGE");
            playerInRange = false;
        }
    }

    IEnumerator enemyHurt()
    {
        gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
    }
    


}
