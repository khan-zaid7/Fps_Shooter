using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera cam;



    [SerializeField]
    private float range;

    private RaycastHit hit;

    [SerializeField]
    private Transform muzzelPosition;

    private int enemyDamage = 10;

    [SerializeField]
    private GameObject muzzelFlash;

    private Animator animator;

    [SerializeField]
    private PlayerMovement pl;

    [SerializeField]
    private bool firing = false;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
        pl = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButtonDown("Fire1"))
        {

            muzzelFlash.SetActive(true);
            firing = true;
            shoot();
            
            
            Debug.Log(firing);
        }
        else
        {
            muzzelFlash.SetActive(false);
            
        }

        animator.SetFloat("Speed", pl.playerSpeed);
        animator.SetBool("isFire",firing);
        firing =false;
    }

   void shoot()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit ,range))
        {
            
            Vector3 hitPoint = hit.point;

            enemyScript em = hit.transform.GetComponent<enemyScript>();
            
            if(em != null)
            {
                
                em.Damage(enemyDamage);
                
            }


        }
    }
}
