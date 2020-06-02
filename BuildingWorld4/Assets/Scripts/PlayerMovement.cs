using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //The controller that we use to control the player
    public CharacterController controller;

    //Movement speed of the player
    public float speed = 12f;

    //Strength of the gravity (used for falling)
    public float gravity = -9.81f;

    //How high we will be able to jump
    public float jumpHeight = 3f;

    //The location of where we check if we are on the ground (often the feet)
    public Transform groundCheck;

    //How big the physicsSphere is where we check if we are on the ground
    public float groundDistance = 0.4f;

    Vector3 velocity;
    bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //We check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance);

        //Make sure we stay on the ground by adding some velocity while on the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -3f;
        }

        //Get the movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //where we need to move based on the input
        Vector3 move = transform.right * x + transform.forward * z;

        //Move based on the input
        controller.Move(move * speed * Time.deltaTime);

        //Jump if we are grounded and press the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //Move down based on the amount of velocity
        controller.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
    }
}
