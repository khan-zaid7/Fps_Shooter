using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Camera cam;



    private float mouseX;
    private float mouseY;

    public float horizontalSpeed;
    public float verticalSpeed;


    private float xRotation;
    private float yRotation;

    public Transform tr;
    public CharacterController ch;

    public float moveSpeed;

    public float gravityForce = 9.8f;

    private Vector3 velocity;
    
    public bool isGrounded;

    public LayerMask Ground;

    private float sphereRadious = 0.4f;

    public Transform GroundCheckPosition;

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
    }

    void player_rotate()

    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;


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
        isGrounded = Physics.CheckSphere(GroundCheckPosition.position, sphereRadious, Ground);
    }
}
