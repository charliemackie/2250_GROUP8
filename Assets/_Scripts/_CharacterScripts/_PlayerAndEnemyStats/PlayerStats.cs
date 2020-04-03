using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    #region Singleton

    public static PlayerStats instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of PlayerController found!");
            return;
        }
        instance = this;
    }

    #endregion

    public GameObject player;

    // Set the move speed
    private float moveSpeed = 5f;

    public GameObject prefab; // the game object's prefab
    public GameObject sword;

    // Declare current health
    public int currentHealth;

    //public bool takingDamage;

    public float timer = 2;     // The time at which the player may attack again (every two seconds the sword may attack an enemy)

    public static bool attack = false;

    private bool hasRespawned;

    private bool mouseActive;

    // Create statsUI to update the stats
    StatsUI statsUI;

    public Xp xpBar;

    public HealthBar healthBar;

    public GameObject modifyMenuUI;

    public GameObject charlie;

    public Transform respawn;

    private GameObject lastEnemy;

    CharacterExperience playerXp;

    private int killCount;
    
    // Start is called before the first frame update
    void Start()
    {
        mouseActive = false;
        hasRespawned = false;
        killCount = 0;
        playerXp = GetComponent<CharacterExperience>();
        statsUI = StatsUI.instance;
        currentHealth = getMaxHealth();
        healthBar.SetMaxHealth(getMaxHealth());
        xpBar.SetXp(playerXp.xp);
    } 

    // Update is called once per frame
    void Update()
    {
        if (mouseActive == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (mouseActive == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        //if(takingDamage)
        //{
        //    //TakeDamage(10);
        //}

        // Timer that only allows the player to attack every 2 seconds
        // Essentially so that the player can't spam left shift
        attack = false;
        timer -= Time.deltaTime;

        if (timer < 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            timer = 2;
            StartCoroutine(SwordAnimation());
            attack = true;
        }
    }

    // add a enemy kill when the player kills and enemy 
    public void killedEnemy(CharacterExperience enemyXp)
    {
        killCount++;
        playerXp.stealExp(enemyXp);
        xpBar.SetXp(playerXp.xp);
        
        if (enemyXp.gameObject == lastEnemy.gameObject)
        {
            hasRespawned = false;
        }
    }

    /// <summary>
    /// Sword prefab animation that pops up when the player attacks
    /// </summary>
    /// <returns></returns>
    IEnumerator SwordAnimation()
    {
        Vector3 pos = new Vector3(-0.9f, 0.6f, 1.03f);
        Quaternion rot = Quaternion.Euler(-50, 0, 90);
        sword = Instantiate(prefab) as GameObject;
        sword.transform.parent = GameObject.Find("FirstPersonPlayer").transform;
        sword.transform.localPosition = pos;
        sword.transform.localRotation = rot;
        yield return new WaitForSeconds(0.5f);
        Destroy(sword);
    }

    // Bring up the menu when a coin is collected
    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;

        if (other.CompareTag("Health"))
        {
            AudioManager.instance.Play("CoinPickup");
            Destroy(other.gameObject);
            addHealth();
        }

        if (other.CompareTag("xp"))
        {
            AudioManager.instance.Play("CoinPickup");
            playerXp.stealExp(other.GetComponent<CharacterExperience>());
            Destroy(other.gameObject);
            xpBar.SetXp(playerXp.xp);
        }

        // Check if the coin is picked up
        if (other.CompareTag("coin"))
        {
            AudioManager.instance.Play("CoinPickup");
            mouseActive = true;

            // Collect coin xp
            playerXp.stealExp(other.GetComponent<CharacterExperience>());

            // Adjust xpBar
            xpBar.SetXp(playerXp.xp);

            // Destroy coin and bring up temporary menu
            Debug.Log("The coin has been picked up");
            Destroy(other.gameObject);
            modifyMenuUI.SetActive(!modifyMenuUI.activeSelf);

            // pause the rest of the game
            Time.timeScale = 0;
        }

        // Note when the player is taking damage from the enemy
        if (other.CompareTag("Enemy1") || other.CompareTag("Enemy2"))
        {
            lastEnemy = other.gameObject;
            Debug.Log(lastEnemy.tag);
        }
    }

    //// Note when the player is no longer taking damage from the enemy
    //private void OnTriggerExit(Collider other)
    //{

    //    if (other.CompareTag("Enemy1") || other.CompareTag("Enemy2"))
    //    {
    //        EnemyStats enemy = other.transform.parent.gameObject.GetComponent<EnemyStats>();
    //        enemy.attack = false;
    //        takingDamage = false;
    //    }
    //}

    // Take damage / reduce healthbar
    public void TakeDamage(int damage)
    {
        AudioManager.instance.Play("Hurt");
        currentHealth -= (damage - defense);
        healthBar.SetHealth(currentHealth);
    }

    // Add to speed if chosen
    public void OnSpeedModifyButton()
    {
        speed = speed + 1;
        moveSpeed += speed;
        Debug.Log("Speed has been added");
        modifyMenuUI.SetActive(false);
        Time.timeScale = 1;
        statsUI.UpdateStats();
        mouseActive = false;
    }

    // Add to strength if chosen
    public void OnStrengthModifyButton()
    {
        strength = strength + 5;
        Debug.Log("Strength has been added");
        modifyMenuUI.SetActive(false);
        Time.timeScale = 1;
        statsUI.UpdateStats();
        mouseActive = false;
    }

    // Add to defense if chosen
    public void OnDefenseModifyButton()
    {
        defense = defense + 1;
        Debug.Log("Defense has been added");
        modifyMenuUI.SetActive(false);
        Time.timeScale = 1;
        statsUI.UpdateStats();
        mouseActive = false;
    }

    override public void Die()
    {
        if (hasRespawned == false)
        {
            base.Die();
            currentHealth = getMaxHealth();
            healthBar.SetHealth(currentHealth);
            GetComponent<Transform>().position = respawn.position;

            CharacterExperience enemyXp = lastEnemy.GetComponent<CharacterExperience>();
            enemyXp.stealExp(playerXp);            
           
            xpBar.SetXp(playerXp.xp);           
            hasRespawned = true;
        }
        else if (hasRespawned == true)
        {
            base.Die();
            SceneManager.LoadScene("Main");
        }

        
    }

    public void addHealth()
    {
        currentHealth += 10;
        healthBar.SetHealth(currentHealth);
    }

    public float getSpeed()
    {
        return moveSpeed;        
    }
}
