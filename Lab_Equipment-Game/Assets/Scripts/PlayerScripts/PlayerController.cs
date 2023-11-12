using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player atributes
    public float playerSpeed;
    public float jumpForce;
    public CharacterController controller;
    public float dashForce;
    //public Rigidbody rb;

    //Movement Variables
    private Vector3 moveDirection;
    public float gravityScale;

    void Start()
    {
        //RigidBody
        //rb = GetComponent<Rigidbody>();

        //Gets aspects of the player
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Original Movement using rigidbody
        /*rb.velocity = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, rb.velocity.y, Input.GetAxis("Vertical") * playerSpeed);

        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }*/

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, moveDirection.y, Input.GetAxis("Vertical") * playerSpeed);

        //Stores the Y value for later
        float yStore = moveDirection.y;

        // gets tyhe direction of the player, moves it Vertically ans horizontally, normalizes the speed, adds back in the y value
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * playerSpeed;
        moveDirection.y = yStore;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("shifty");
            controller.Move((moveDirection * dashForce) * Time.deltaTime);
        }


        if(!controller.isGrounded)
        {
            while(Time.deltaTime < 1)
            {
                gravityScale = 0.005f;
            }
            gravityScale = 0.5f;
        }

        if(controller.isGrounded)
        {
            gravityScale = 2;
        }

        // checks for ground and if Space is pressed
        if(controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            // Resets the gravity scale to 0
            moveDirection.y = 0f;

            // applise jump
            moveDirection.y = jumpForce;
        }

        // jump physics
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
