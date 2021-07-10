using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera cam;

    private Transform muzzelPosition;

    [SerializeField]
    private float range;

    private RaycastHit hit;

    
    public Enemy enemy;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0) && Physics.Raycast(cam.transform.position, cam.transform.forward, out hit , range))
        {
            if(hit.collider.tag == "Enemy")
            {
                enemy.currentHealth -= 10;
                Debug.Log("Hit the enemy");
            }
        }
    }
}
