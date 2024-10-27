using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 1, -10);
 
    }


    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

}
