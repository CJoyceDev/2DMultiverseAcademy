using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideL : MonoBehaviour
{
    [SerializeField] PlayerMovementPD pm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            pm.wallSlideL = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            pm.wallSlideL = false;
        }
    }
}
