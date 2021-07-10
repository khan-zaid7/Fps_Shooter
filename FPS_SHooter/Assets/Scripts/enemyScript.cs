using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    private float fullHealth = 50;

    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <=0)
        {
            Destroy(this.gameObject);
        }
    }

}
