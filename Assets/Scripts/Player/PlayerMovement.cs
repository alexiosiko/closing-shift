using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
public float baseSpeed = 2f;
    public float gravity = -4f;
    public float sprintSpeed = 5f;
    // public float jumpHeight = 1.5f; // Optional: Add jumping
    float speedBoost = 1f;
    float verticalVelocity = 0f;
    public Vector3 moveDelta = Vector3.zero;

    void Update()
    {
        if (StatusManager.Singleton.GetFreeze())
            return;
        MovePlayer();
    }

    void MovePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Fire3"))
            speedBoost = sprintSpeed;
        else
            speedBoost = 1f;

        // Movement on the X and Z axes
        moveDelta = transform.right * x + transform.forward * z;

        // Check if the player is grounded
        if (controller.isGrounded)
        {
            // Reset vertical velocity when grounded
            verticalVelocity = -2f;

            // Handle jumping (optional)
            // if (Input.GetButtonDown("Jump"))
            // {
            //     verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // }
        }
        else
            // Apply gravity when not grounded
            verticalVelocity += gravity * Time.deltaTime;

        // Apply the vertical velocity to moveDelta
        moveDelta.y = verticalVelocity;

        // Move the player
        controller.Move(moveDelta * (baseSpeed + speedBoost) * Time.deltaTime);
    }

    void Awake()
    {
        Singleton = this;
        controller = GetComponent<CharacterController>();
    }

    public static PlayerMovement Singleton;
    CharacterController controller;
}