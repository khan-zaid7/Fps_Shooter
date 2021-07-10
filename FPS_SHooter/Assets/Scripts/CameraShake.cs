using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    //Shake Camera With Player Movement

    //Strength of the camera Shake 
    [SerializeField]
    private float strength = 5f;

    //duration of the camera shake effect
    private float duration=1.0f;

    //slow down the duration in every frame
    private float slowDownDuration=0.1f;

    //camera will shake only when shouldShake is true 
    public bool shouldShake = false;

    //the initial duration of the camera
    private float initialDuration;

    //the initial position of the camera
    private Vector3 initialCameraPosition;

    //refrence to the camera gameObject
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        initialCameraPosition = cam.transform.localPosition;
        initialDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShake)
        {
            if(duration>0)
            {
                cam.transform.localPosition = Random.insideUnitCircle * strength;
                duration -= slowDownDuration *Time.deltaTime;
            }
            else
            {
                shouldShake = false;
                cam.transform.localPosition = initialCameraPosition;
                duration = initialDuration;
            }
        }
    }
}
