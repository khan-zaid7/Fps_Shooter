using UnityEngine;

public class AmmoAndReload : MonoBehaviour
{
    [SerializeField]
    public  int fullAmmo;

    public int currentAmmo;
    public int lowAmmo = 0; 

    //refernce to the HUD image 
    [SerializeField]
    public Sprite image;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = fullAmmo;
    }
    public void reload()
    {
        currentAmmo = fullAmmo;
    }
}
