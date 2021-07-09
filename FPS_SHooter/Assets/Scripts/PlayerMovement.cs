using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Refrence to the Camera
    private Camera cam;


    //Mouse Input On xAxis 
    private float mouseX;
    
    //Mouse Input On yAxis 
    private float mouseY;

    //Player Move Speed multiplier On the xAxis
    [SerializeField]
    private float horizontalSpeed;

    //Player Move Speed multiplier On the zAxis
    [SerializeField]
    private float verticalSpeed;

    //rotation of the player on the xAxis
    private float xRotation;

    //rotation of the player on the yAxis
    private float yRotation;

    //Refrence to the Player Game Object to rotate along the yAxis with camera
    public Transform tr;

    //Refrence to the CharacterController component On the playerObject
    public CharacterController ch;

    //Speed of the player while walking
    [SerializeField]
    private float walkSpeed;

    //Speed of the player while running
    [SerializeField]
    private  float runSpeed;

    //Current speed of the player
    private float moveSpeed;

    //bool variable to check if player is Running
    private bool isRunning;

    //Amount of Gravity Apply on the player every frame
    public float gravityForce = 9.8f;

    //Vector3 velocity of player for jump and gravity
    private Vector3 velocity;
    
    //Bool variable to check if player is on ground or not
    public bool isGrounded;

    //A refrence to the Ground layer
    public LayerMask whatIsGround;

    //the radious of the groundCheck sphere     
    private float sphereRadious = 0.4f;

    //the position where the sphere is instansiated
    public Transform GroundCheckPosition;



    [SerializeField]
    private float climbSpeed;

    [SerializeField]
    private float sticToWall;

    [SerializeField]
    private float range;


    // Start is called before the first frame update
    void Start()
    {
        
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        player_rotate();    
        player_move();
        Gravity();
        GroundCheck();
        Climb();
    }

    void player_rotate()

    {
        
        mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;


        tr.Rotate(Vector3.up * mouseX);
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
 
        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
       
    }

    void player_move()

    {
        float h_input = Input.GetAxis("Horizontal");
        float v_input = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h_input + transform.forward * v_input ;

        if(getRunKey())
        {
            isRunning = !isRunning;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = !isRunning;
        }

        if(isRunning)
        {
            moveSpeed = runSpeed;
        }
        else 
        {
            moveSpeed = walkSpeed;
        }
        ch.Move(move * moveSpeed * Time.deltaTime);
    }

    void Gravity() 
    {
        if(isGrounded)
        {
            velocity.y = 0;

        }
        else 
        {
            velocity.y -= gravityForce * Time.deltaTime;
        }

        ch.Move(velocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(GroundCheckPosition.position, sphereRadious, whatIsGround);
    }

    bool getRunKey()
    {
       return Input.GetKeyDown(KeyCode.LeftShift);
    }

    void Climb()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if(hit.transform.tag == "climbObject")
            {
                if(Input.GetKey(KeyCode.Tab))
                {
                    gravityForce = 1f;
                    Vector3 climb = transform.forward * sticToWall + transform.up * climbSpeed;

                    ch.Move(climb * Time.deltaTime);

                }
            }
        }
        else
        {
            gravityForce = 9.8f;
        }

    }


}
