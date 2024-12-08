using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTriggerActivate : MonoBehaviour
{

    CJMovingPlatform mp;

    private void Awake()
    {
        mp = GetComponentInParent<CJMovingPlatform>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mp != null)
            {
                mp.isActive = true;
            }
        }
    }
}
