using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    private float fullHealth = 50;

    private float currentHealth;

    [SerializeField]
    private float targetReapperTime = 2f;

    private float nextTime;
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
            Destroy(gameObject);
            if (Time.time > nextTime)
            {
                nextTime = Time.time + targetReapperTime;
                instanciateGameObject();
            }
               
        }
    }

    public void instanciateGameObject()
    {
        Instantiate(this.gameObject);
        Debug.Log("chal rhna hai 0");
    }

}
