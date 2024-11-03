using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    //Stuff declared //PD
    public Transform player;

    Vector3 defaultPosition;
    Vector3 changedPosition;

    Vector3 defaultDistance = new Vector3(0, 1, -6);
    Vector3 changedDistance = new Vector3(0, 1, -6);

    public bool isCameraDistanceChanged = false;
    public bool isCameraPositionChanged = false;

    //Cap Framerate to 30, and make player position easier to get //PD
    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        changedPosition = player.transform.position;
    }

   
    // Does the camera swapping of numbers //PD
    void Update()
    {
        defaultPosition = player.transform.position;

        Vector3 x;
        if (isCameraPositionChanged)
        {
            x = changedPosition;
        }
        else
        {
            x = defaultPosition;
        }
        Vector3 y;
        if (isCameraDistanceChanged)
        {
            y = changedDistance;
        }
        else
        {
            y = defaultDistance;
        }
        transform.position = x + y;
 
    }

    // checks what values are to be swapped and to what they are going to be swapped, called in trigger script //PD
    public void changeCamera(bool x, Vector3 position, bool y, Vector3 distance)
    {
        if (x)
        {
            changedPosition = position;
            
        }
        isCameraPositionChanged = x;


        if (y)
        {
            changedDistance = distance;
            
        }
        isCameraDistanceChanged = y;

    }


    

}
