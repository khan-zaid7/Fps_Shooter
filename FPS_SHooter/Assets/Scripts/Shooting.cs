using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    //Refrence to the main camera for shooting the raycast 
    [SerializeField]
    private Camera cam;

    //how far the bulltet or raycast can move
    [SerializeField]
    private float range;

    //it contains the information of the raycast hit point 
    private RaycastHit hit;

    //the transform from where the gunFire effect will play
    [SerializeField]
    private Transform muzzelPosition;

    //how much health is deducted when raycast hits the enemy 
    [SerializeField]
    private int enemyDamage = 10;

    //the refrence to the fire partile
    [SerializeField]
    private GameObject muzzelFlash;

    //the refrence to the animator componenet to play diffrent animations
    private Animator animator;

    // refrence to the player script 
    [SerializeField]
    private PlayerMovement pl;

    //a bool variable which defines if the player is firing 
    [SerializeField]
    private bool firing = false;

    //the amount time player has to wait for the next fire 
    private float nextFire;

    //the rate of firing 
    [SerializeField]
    private float fireRate = 0.25f;

    //refrence to the AmmoAndReload script 
    [SerializeField]
    private AmmoAndReload ammoAndReload;

    //the recoil on up dir applied when plyaer shoots
    [SerializeField]
    private float upRecoil;

    //the recoil on side dir applied when player shoots
    [SerializeField]
    private float sideReciol;

    // Start is called before the first frame update
    void Start()
    {
        //set the camera transform to the main camera 
        cam = Camera.main;

        //Get the animator component attached to this object 
        animator = GetComponent<Animator>();

        //get the playerMovement script component from the parent gameObject
        pl = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if the player press mouse[0] key and time is greater then the next fire and Current ammo is greater then 0
        if(Input.GetButton("Fire1") && Time.time > nextFire && ammoAndReload.currentAmmo > 0)
        {
            //increment the nextFire with the curentTime passed + fireRate 
            nextFire = Time.time + fireRate;

            //assign the sideRecoil value to PlayerMovemnt Script's sideRecoilMultiplier
            pl.sideRecoilMultiplier = sideReciol;

            //assign the upRecoil value to PlayerMovemnt Script's upRecoilMultiplier
            pl.upRecoilMultiplier = upRecoil ;

            

            //acitivate the muzzel flash particle when the player shoots 
            muzzelFlash.SetActive(true);

            //fining becomes true 
            firing = true;
            
            //shoot the raycast by calling the shoot() method  
            shoot();

            //decrement the cuurentAmmo
            ammoAndReload.currentAmmo --;
                
            Debug.Log(ammoAndReload.totalAmmo);
        
        }
        else
        {
            //stop the muzzelFlash particle if the player is not firing 
            muzzelFlash.SetActive(false);
            
        }
        
        //if the ammoAndReload.currentAmmo is less then 30  and totalAmmo is greater then 0
        if (ammoAndReload.currentAmmo < ammoAndReload.magazineCapacity)
        {
            //if user presses the R key 
            if(Input.GetKeyDown(KeyCode.R))
            {
                
                if (ammoAndReload.totalAmmo>ammoAndReload.lowAmmo)
                    //call the reload function defined in the ammoAndReload script 
                    ammoAndReload.reload();
                
            } 
        }

        if(animator != null)
        {
            //play the player run andimation relative to the player speed  var defined in the playerMovement script  
            animator.SetFloat("Speed", pl.playerSpeed);
        
            //play the firing animation 
            animator.SetBool("isFire",firing);
        }

        
        //firing becomes false 
        firing =false;
    }

    //responsible for shooting the raycast 
   void shoot()
    {
        //it shoots a raycast from cam.transform position to the forward direction on zAxis (0,0,1) at a certain range and stores the info 
        //in the hit variable and returns true if ray collides with another gameObject  
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit ,range))
        {
            //get the raycast hit point the space and store in a variable hitPoint
            Vector3 hitPoint = hit.point;

            //????
            //get the enemy script for the hit enemy Object
            enemyScript em = hit.transform.GetComponent<enemyScript>();
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            
            //if ray hits the emeny and stores the refrence in the em 
            if(em != null && rb != null)
            {
                rb.AddForce(Vector3.left * 100);
                //calling the damage function defined in the enemeyScript and passing the enemyDamage value 
                em.Damage(enemyDamage);   

            }
        }
    }
}
