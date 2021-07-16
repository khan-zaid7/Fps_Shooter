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
    
    private int totalWepons;

    //refrence to the magazineCapityText UI element 
    [SerializeField]
    private Text magCapacityTxt;

    //A refrence to the totalAmmoText UI element 
    [SerializeField]
    private Text totalAmmoTxt;

    //refrence to the totaLAmmoText UI element
    [SerializeField]
    private Text CurrentAmmoText;

    //refrence to the currentWepon image UI element
    [SerializeField]
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        totalWepons = wepons.Length ;
        
        //get the Ammo reload script attached to the current wepon
        AmmoAndReload am = wepons[currentWepon].GetComponent<AmmoAndReload>();

        //set the magazineCapacity UI element equal to the currentWepon's AmmoAndReload script's fullAmmo var value
        magCapacityTxt.text = am.magazineCapacity.ToString();

        //set the magazineCapacity UI element equal to the currentWepon's AmmoAndReload script's fullAmmo var value
        totalAmmoTxt.text = am.totalAmmo.ToString();

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
        totalWepons = wepons.Length;

        //call the switchwepon function
        switchWepon();
        AmmoAndReload am = wepons[currentWepon].GetComponent<AmmoAndReload>();
        totalAmmoTxt.text = am.totalAmmo.ToString();
        magCapacityTxt.text = am.magazineCapacity.ToString();
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

                //if cuurentWepon become greater or equal to the totalWepon 
                if(currentWepon >= totalWepons)
                    currentWepon = 0;

                //acticate the new current wepon
                wepons[currentWepon].SetActive(true);
            }
        }

        //Get the mouse scroll up input
        if(Input.GetAxis("Mouse ScrollWheel")>0)
        {
            //if the value of current weapon is greater then the 0
            if(currentWepon >= 0)
            {
                //disable the current selected wepon on the screen
                wepons[currentWepon].SetActive(false);

                //decrement the current wepon on scroll up
                currentWepon --;

                if(currentWepon < 0)
                    currentWepon = totalWepons -1;

                    
                //acticate the new current wepon
                wepons[currentWepon].SetActive(true);
            }

        }
    }
}
