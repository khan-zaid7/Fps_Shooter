using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float fullHealth = 50;

    
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentHealth <=0)
        {
            Destroy(gameObject);
        }
    }

}
