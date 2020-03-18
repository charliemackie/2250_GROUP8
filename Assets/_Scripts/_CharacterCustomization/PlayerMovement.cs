using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Access the player controller
    public CharacterController controller;

    // Import playerStats for moveSpeed
    PlayerStats playerStats;

    // Declare physics variables
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    // Declare variables for ground checking
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        // Instatiate the playerStats
        playerStats = PlayerStats.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Create tiny sphere. If it collides with it, grounded will be true.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Check ground and reset velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get the horizontal and vertical views
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Allow the player to move
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the player depending on speed
        Debug.Log(playerStats.getSpeed());
        controller.Move(move * playerStats.getSpeed() * Time.deltaTime);

        // Allow the player to jump if grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Implement gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
