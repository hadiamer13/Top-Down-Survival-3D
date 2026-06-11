using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    
    private Rigidbody rb;
    private Vector2 MoveInput;
    private float JumpInput;

    bool isGrounded;
    [Header("Attribute Enablers")]
    [SerializeField] private bool isJumpingEnabled = true;
    [SerializeField] private bool isStrongerGravity = true;

    [Header("Movement Modifiers")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float gravityMultiplier = 2.5f;

    [Header("Jumping Mechanics")]
    [SerializeField] private Transform GroundCheckSphere;   // position of an invisible object below the player
    [SerializeField] private float GroundCheckRadius = 0.2f;   // radius for fround detection
    [SerializeField] private LayerMask GroundLayer; // Layer Masks checks interaction with a specific Layer in unity

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        
        CheckGrounded();
        StrongerGravity();
        PlayerMovement();
    }

    // Input System Function for movement action
    private void OnMovement(InputValue inputValue)
    {
        MoveInput = inputValue.Get<Vector2>();
    }

    // Input system Function for Jump
    private void OnJump(InputValue inputValue)
    {
        JumpInput = inputValue.Get<float>();
        if (JumpInput == 1 && isGrounded && isJumpingEnabled)
        {
            rb.AddForce(new Vector3(0, JumpInput * jumpHeight, 0), ForceMode.Impulse);
            JumpInput = 0;
        }
        else
        {
            JumpInput = 0;
        }

    }

    private void PlayerMovement()
    {
        // extra line to simplify directions
        Vector3 moveDirection = new Vector3(MoveInput.x,0,MoveInput.y);

        rb.linearVelocity =new Vector3(moveDirection.x * moveSpeed ,rb.linearVelocity.y , moveDirection.z * moveSpeed);
        
    }


    private void StrongerGravity()
    {
        if(isStrongerGravity)
        {
            // if rb is falling down
            if (rb.linearVelocity.y < 0 && !isGrounded)
            {
                float speedX = rb.linearVelocity.x;
                float speedZ = rb.linearVelocity.z;
                float speedY = rb.linearVelocity.y + (Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime);
                rb.linearVelocity = new Vector3(speedX, speedY, speedZ);
            }
        }
       
    }

    // draws a wired sphere in editor to see and debug the radius of collision of anything
    private void OnDrawGizmosSelected()
    {
        if(GroundCheckSphere != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GroundCheckSphere.position, GroundCheckRadius);
        }
    }

    // check if player is grounded
    private void CheckGrounded()
    {
        // checkSphere, creates an imaginary sphere at position GroundCheckSphere of radius GroundCheckRadius and checks if sphere
        // interacts with LayerMask GroundLayer. if it does, it returns 1;
        isGrounded = Physics.CheckSphere(GroundCheckSphere.position, GroundCheckRadius, GroundLayer);
    }

    // modify add constraint so that backward movement speed is reduced to 60 %

}
