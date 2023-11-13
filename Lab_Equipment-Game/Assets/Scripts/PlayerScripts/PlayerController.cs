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
    public bool hasDash = false;

    //public Rigidbody rb;

    //Movement Variables
    private Vector3 moveDirection;
    public float gravityScale;


    //bool
    public bool hasDashed = false;
    public bool isPaused = false;


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

        Debug.Log(moveDirection);

        if(controller.isGrounded)
        {
            hasDashed = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && !hasDashed)
        {
            Debug.Log("shifty");
            controller.Move((new Vector3(moveDirection.x, 0, moveDirection.z) * dashForce) * Time.deltaTime);
            hasDashed = true;
        }

        // checks for ground and if Space is pressed
        if(controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            hasDash = true;
            // Resets the gravity scale to 0
            moveDirection.y = 0f;

            // applise jump
            moveDirection.y = jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && hasDash)
        {
            controller.Move(moveDirection * dashForce * Time.deltaTime * 2);
            hasDash = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && controller.isGrounded)
        {
            Emote();
        }

        //Detect pause input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                UIManager.Instance.PauseGame();
            }
            else
            {
                UIManager.Instance.UnPauseGame();
            }
        }

        // jump physics
        if (!controller.isGrounded)
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Emote()
    {

    }
}
