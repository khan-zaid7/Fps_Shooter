using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImCameraSwitch : MonoBehaviour
{

    [SerializeField]
    private Transform GunPosition;

    [SerializeField]
    private Transform AimPosition;

    private Vector3 gunOldPosition;
    [SerializeField]
    private float AimSpeed;

    void Start()
    {
        gunOldPosition = GunPosition.transform.position;
    }

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            GunPosition.transform.position = Vector3.Lerp(gunOldPosition, AimPosition.transform.position, Time.deltaTime * AimSpeed );
        }
        else if(Input.GetMouseButtonUp(1))
        {
             GunPosition.transform.position = Vector3.Lerp(GunPosition.transform.position, gunOldPosition, Time.deltaTime * AimSpeed );
        }

    }
}
