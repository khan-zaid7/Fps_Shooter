using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponHandler : MonoBehaviour
{
    private int currentWepon=0;

    [SerializeField]
    private GameObject[] wepons;

    [SerializeField]
    private int totalWepons = 1;
    // Start is called before the first frame update
    void Start()
    {

        //loop through each wepon and disble them at the start
        foreach(GameObject i in wepons)
        {
            i.SetActive(false);
        }

        //set the wepon to currentWepon
        wepons[currentWepon].SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        //call the switchwepon function
        switchWepon();
    }

    void switchWepon()
    {
        //Get the mouse scroll down input
        if(Input.GetAxis("Mouse ScrollWheel")<0)
        {
            //if the index of current weapon is smaller then the total wepons
            if(currentWepon < totalWepons)
            {
                //disable the current selected wepon on the screen
                wepons[currentWepon].SetActive(false);

                //increment the current wepon on scroll down
                currentWepon ++;

                //acticate the new current wepon
                wepons[currentWepon].SetActive(true);
            }
            //if the currentWepon counter var becomes greater then totalWepon
            else
            {
                //disable the selected wepon
                wepons[currentWepon].SetActive(false);
                
                //set cuurentwepon index to zero
                currentWepon = 0;
                //activate the first wepon on the array
                wepons[currentWepon].SetActive(true);
            }
        }

        //Get the mouse scroll up input
        if(Input.GetAxis("Mouse ScrollWheel")>0)
        {
            //if the value of current weapon is greater then the 0
            if(currentWepon > 0)
            {
                //disable the current selected wepon on the screen
                wepons[currentWepon].SetActive(false);

                //decrement the current wepon on scroll up
                currentWepon --;

                //acticate the new current wepon
                wepons[currentWepon].SetActive(true);
            }
            //if the currentWepon counter var becomes smaller then 0
            else
            {
                //disable the selected wepon
                wepons[currentWepon].SetActive(false);
                
                //set cuurentwepon value to totalwepon value
                currentWepon = totalWepons;

                //activate the first wepon on the array
                wepons[currentWepon].SetActive(true);
            }
        }
    }
}
