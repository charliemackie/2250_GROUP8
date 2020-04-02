/*
 * This class models all of the enemy's statistics
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    public int currentHealth;       // The current health of the enemy
    public bool playerInRange;      // Determining whether or not the player is in range of the enemy
    public bool attack;
    public int hitStrength = 10;
    public int enemyMaxHealth = 100;
    private bool wait = false;
    Collider player;

    Animator m_Animator;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        currentHealth = enemyMaxHealth;
        m_Animator = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // If the current health is less than 0 the enemy needs to die
        if (currentHealth <= 0)
        {
            Die();
        }

        // If the player is in range of the enemy and the player is attacking the enemy than it must deal damage
        if (playerInRange && PlayerStats.attack == true){
            currentHealth -= PlayerStats.hitStrength;
            StartCoroutine(EnemyHurt());
        }

        if(attack && !wait)
        {
            PlayerStats playerStats = player.gameObject.GetComponent<PlayerStats>();
            playerStats.TakeDamage(hitStrength);
            wait = true;
            m_Animator.SetTrigger("Attack");
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2f);
        wait = false;
    }

    /// <summary>
    /// Overriding the die method of the base to destroy the enemy once it dies
    /// </summary>
    override public void Die(){
        m_Animator.SetTrigger("Die");
        StartCoroutine(pauseDeath());
        PlayerStats.instance.killedEnemy(GetComponent<CharacterExperience>());
        base.Die();
    }

    /// <summary>
    /// On the collision of the enemy
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        player = other;
        // Note when the player is in range
        if (other.CompareTag("Player"))
        {
            attack = true;
            print("IN RANGE");
            playerInRange = true;
        }
    }

    /// <summary>
    /// On the exit collision of the enemy
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        player = other;
        // Note when the player is out of range
        if (other.CompareTag("Player"))
        {
            attack = false;
            print("OUT OF RANGE");
            playerInRange = false;
        }
    }

    /// <summary>
    /// Animation turning the enemy red when its hit
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyHurt()
    {
        gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
    }

    public void GunDamage(int damageAmount)
    {
        if (!gameObject.CompareTag("Enemy0"))
        {
            currentHealth -= damageAmount;
            StartCoroutine(EnemyHurt());
            if (currentHealth <= 0)
            {
                Die();
            }
        }else{
            m_Animator.SetTrigger("Block");
        }
    }

    IEnumerator pauseDeath()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

}
