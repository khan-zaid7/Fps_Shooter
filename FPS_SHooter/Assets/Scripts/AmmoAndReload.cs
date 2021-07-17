using UnityEngine;

public class AmmoAndReload : MonoBehaviour
{
    [SerializeField]
    public  int magazineCapacity;

    public int currentAmmo;
    public int lowAmmo = 0; 

    public int totalAmmo;

    //refernce to the HUD image 
    [SerializeField]
    public Sprite image;

    public int neededAmmo;

    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magazineCapacity;
        Debug.Log(totalAmmo);
    }
    public void reload()
    {
        //if totalAmmo itself have enough bullets
        if (totalAmmo>=magazineCapacity)
        {
            //get the ammo required to fill the magazine
            neededAmmo = magazineCapacity - currentAmmo;
            //decrement the ammo required to fill the magazine from total 
            totalAmmo -= neededAmmo;
            //then increment the ammo required to fill the magazine  
            currentAmmo += neededAmmo;
        }
        //if total ammo is less then the magazineCapacity
        else if(totalAmmo>0)
        {
            //get the ammo required to fill the magazine
            neededAmmo = magazineCapacity - currentAmmo;
            //if totalAmmo has enough bullets required to fill the magazine
            if(neededAmmo<totalAmmo)
            {
                totalAmmo -= neededAmmo;
                currentAmmo += neededAmmo;
            }
            //if totalAmmo is less then the required neededAmmo then incerement all the bullets in the currentAmmo from totalAmmo
            else 
            {
                currentAmmo += totalAmmo;
                totalAmmo = 0;
            }
        }
    }
}
