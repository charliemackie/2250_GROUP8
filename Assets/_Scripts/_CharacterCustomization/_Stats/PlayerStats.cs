using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Set the move speed
    private float moveSpeed = 5f;

    public GameObject prefab; // the game object's prefab
    public GameObject sword;

    // Declare current health
    public int currentHealth;

    public bool takingDamage;

    // Create statsUI to update the stats
    StatsUI statsUI;

    public HealthBar healthBar;

    public GameObject modifyMenuUI;
    
    // Start is called before the first frame update
    void Start()
    {
        statsUI = StatsUI.instance;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if(takingDamage){
            TakeDamage(2);
        }

        StartCoroutine(swordAnimation());
    }

    IEnumerator swordAnimation()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
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
        yield return new WaitForSeconds(0);
    }

    // Bring up the menu when a coin is collected
    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;

        // Check if the coin is picked up
        if (other.CompareTag("coin"))
        {
            // Destroy coin and bring up temporary menu
            Debug.Log("The coin has been picked up");
            Destroy(other.gameObject);
            modifyMenuUI.SetActive(!modifyMenuUI.activeSelf);

            // pause the rest of the game
            Time.timeScale = 0;
        }

        if (other.CompareTag("Enemy1") || other.CompareTag("Enemy2"))
        {
            takingDamage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Enemy1") || other.CompareTag("Enemy2"))
        {
            takingDamage = false;
        }
    }

    // Take damage / reduce healthbar
    void TakeDamage(int damage)
    {
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
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Add to strength if chosen
    public void OnStrengthModifyButton()
    {
        strength = strength + 1;
        Debug.Log("Strength has been added");
        modifyMenuUI.SetActive(false);
        Time.timeScale = 1;
        statsUI.UpdateStats();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Add to defense if chosen
    public void OnDefenseModifyButton()
    {
        defense = defense + 1;
        Debug.Log("Defense has been added");
        modifyMenuUI.SetActive(false);
        Time.timeScale = 1;
        statsUI.UpdateStats();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public float getSpeed()
    {
        return moveSpeed;
    }
}
