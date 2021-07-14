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
    private float walkSpeed = 3f;

    //Speed of the player while running
    [SerializeField]
    private  float runSpeed = 8f;

    //Current speed of the player
    private float moveSpeed;

    //bool variable to check if player is Running
    private bool isRunning;

    //Amount of Gravity Apply on the player every frame
    public float gravityForce = 9.8f;

    //Vector3 velocity of player for jump and gravity
    private Vector3 Velocity;
    
    //Bool variable to check if player is on ground or not
    public bool isGrounded;

    //A refrence to the Ground layer
    public LayerMask whatIsGround;

    //the radious of the groundCheck sphere     
    private float sphereRadious = 0.4f;

    //the position where the sphere is instansiated
    public Transform GroundCheckPosition;




    //Camera Shake

    [SerializeField]
    private CameraShake cameraShake;
    public float playerSpeed;

    private Vector3 lastPosition;
    private Vector3 currentPosition;

    //Add a recoil on the sides when a player shoots, value differs for differnt guns
    [HideInInspector]
    public float sideRecoilMultiplier = 0;

    //Add a recoil on the up when a player shoots, value differs for differnt guns
    [HideInInspector]
    public float upRecoilMultiplier = 0;


    //Stairs Gravity

    public bool onStaris = false;

    public float gravityOnStairs;

    public LayerMask whereIsStairs;
    void Start()
    {
        //get refrence to the main camera Object
        cam = Camera.main;

        //lock the cursor at the center 
        Cursor.lockState = CursorLockMode.Locked;

        //currentPosition is the transform.position
        currentPosition = tr.position;

        //lastPosition is the transform.position
        lastPosition = tr.position;
    }

    // Update is called once per frame
    void Update()
    {
        player_rotate();    

        player_move();

        StairsCheck();
        Gravity();
        GroundCheck();
        shakeCameraOnMove();

    }

    void player_rotate()

    {
        //Get the mouse input on the xAxis and add sideRecoil only when player shoots 
        mouseX = sideRecoilMultiplier + Input.GetAxis("Mouse X") * horizontalSpeed;

        //Get the mouse input on the yAxis and add up recoil only when player shoots 
        mouseY = upRecoilMultiplier +  Input.GetAxis("Mouse Y") * verticalSpeed;

        //Rotate the player gameObject with the mouseX movement to face the camera's direction
        tr.Rotate(Vector3.up * mouseX);


        yRotation += mouseX;
        xRotation -= mouseY;

        //prevent the player from facing up and down above and beyound -90 and 90
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //rotate the camera gameObject on the x and y axis 
        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);

        //make the sideRecoil back to  0 once the bullet is shooted
        sideRecoilMultiplier = 0;

        //make the upRecoil back to  0 once the bullet is shooted
        upRecoilMultiplier = 0;
    }

    //responsible for player movement 
    void player_move()
    {
        //Get the input on the xAxis from -1 to 1
        float h_input = Input.GetAxis("Horizontal");

        //Get the input on the zAxis from -1 to 1
        float v_input = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h_input + transform.forward * v_input ;

        //if player press leftShift then isRunning = true until the player has pressed key
        if(getRunKey())
        {
            isRunning = true;
        }
        //if the leftShift is released then isRunning = false
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        //if isRunning is true then move the player at the runSpeed
        if(isRunning)
        {
            //Slowly increase the currentSpeed(moveSpeed) value to runSpeed value

            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, 0.1f);
            
        }
        //if isRunning is false move the player at walkSpeed
        else 
        {
            //Slowly decrease the currentSpeed(moveSpeed) value to walkSpeed value
            
            moveSpeed = Mathf.Lerp(moveSpeed,walkSpeed,0.1f);
        }
        
        ch.Move(move * moveSpeed * Time.deltaTime);
    }

    void Gravity() 
    {
        if(isGrounded)
            Velocity.y = 0;
        else 
            Velocity.y -= gravityForce * Time.deltaTime;

        ch.Move(Velocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        //create a sphere of radious = sphereRadius, at the position of groundCheckPosition, if the sphere collides with whatIsGround it returns true
        isGrounded = Physics.CheckSphere(GroundCheckPosition.position, sphereRadious, whatIsGround);
    }

    void StairsCheck()
    {
        onStaris = Physics.CheckSphere(GroundCheckPosition.position, sphereRadious, whereIsStairs);

        if(onStaris)
            gravityForce = gravityOnStairs;
    }

    bool getRunKey()
    {
       return Input.GetKeyDown(KeyCode.LeftShift);
    }


    void shakeCameraOnMove()
    {
        //the current position is the transform.position
        currentPosition = tr.position;
        
        //it reurns the speed of the player at each frame 
        playerSpeed = (currentPosition - lastPosition).magnitude / Time.deltaTime;

        //after getting the speed lastpositon becomes currentPosition
        lastPosition = currentPosition;
    }


}
 