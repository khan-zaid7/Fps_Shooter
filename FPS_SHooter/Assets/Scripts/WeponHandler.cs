using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeponHandler : MonoBehaviour
{
    //the currentWepon value in the array of wepons 
    private int currentWepon=0;

    //array of wepons gameObject
    [SerializeField]
    private GameObject[] wepons;

    //total number of wepons
    [SerializeField]
    private int totalWepons = 1;

    //refrence to the totaLAmmoText UI element 
    [SerializeField]
    private Text TotalAmmoTxt;

    //refrence to the totaLAmmoText UI element
    [SerializeField]
    private Text CurrentAmmoText;

    //refrence to the currentWepon image UI element
    [SerializeField]
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        //get the Ammo reload script attached to the current wepon
        AmmoAndReload am = wepons[currentWepon].GetComponent<AmmoAndReload>();

        //set the totalAmmo UI element equal to the currentWepon's AmmoAndReload script's fullAmmo var value
        TotalAmmoTxt.text = am.fullAmmo.ToString();

        //set the currentAmmo UI element equal to the currentWepon's AmmoAndReload script's currentAmmo var value
        CurrentAmmoText.text = am.currentAmmo.ToString();

        //set the currentWepon UI image equal to the currentWepon's  AmmoAndReload script's image  var value
        img.sprite = am.image;

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
        AmmoAndReload am = wepons[currentWepon].GetComponent<AmmoAndReload>();
        Debug.Log(currentWepon);
        
        TotalAmmoTxt.text = am.fullAmmo.ToString();
        CurrentAmmoText.text = am.currentAmmo.ToString();
        img.sprite = am.image;


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
