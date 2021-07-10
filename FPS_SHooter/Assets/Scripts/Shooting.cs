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

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            muzzelFlash.SetActive(true);
            shoot();
        }
        else
        {
            muzzelFlash.SetActive(false);
        }

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
