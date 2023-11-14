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
    public int counter = 0;

    private Animation anim;

    //public Rigidbody rb;

    //Movement Variables
    private Vector3 moveDirection;
    public float gravityScale;


    //bool
    public bool hasDashed = false;
    public bool isPaused = false;

    private int characterSelected;
    private GameObject characterModelObject;

    //audio
    public AudioSource jumpSFX;
    public AudioSource footStepSFX;

    void Start()
    {
        //RigidBody
        //rb = GetComponent<Rigidbody>();

        //Gets aspects of the player
        controller = GetComponent<CharacterController>();

        characterSelected = GameObject.Find("CharacterSelector").GetComponent<CharacterSelection>().selectedCharacter;
        transform.GetChild(characterSelected).gameObject.SetActive(true);
        characterModelObject = transform.GetChild(characterSelected).gameObject;

        anim = characterModelObject.GetComponent<Animation>();
        if (anim != null)
            Debug.Log("anim found");
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
        moveDirection = moveDirection * playerSpeed;
        moveDirection.y = yStore;

        if ((moveDirection.x != 0 || moveDirection.z != 0) && controller.isGrounded)
        {
            anim.clip = anim.GetClip("runAnim");
            anim.Play();
            counter++;
        }

        if(counter > 150)
        {
            footStepSFX.Play();
            counter = 0;
        }

        if (!anim.isPlaying && controller.isGrounded)
        {
            anim.clip = anim.GetClip("idleAnim");
            anim.Play();
        }

        if(controller.isGrounded)
        {
            hasDashed = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && !hasDashed)
        {
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

            anim.clip = anim.GetClip("jumpAnim");
            anim.Play();
            jumpSFX.Play();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && hasDash)
        {
            controller.Move(moveDirection * dashForce * Time.deltaTime * 2);
            hasDash = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && controller.isGrounded)
        {
            anim.clip = anim.GetClip("emoteAnim");
            anim.Play();
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
}
