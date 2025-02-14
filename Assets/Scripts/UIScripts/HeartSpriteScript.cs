using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpriteScript : MonoBehaviour
{
    [SerializeField]
    GameObject heart1A, heart1B, heart2A, heart2B, heart3A, heart3B;

    public int healthAmount;
    

    

    // Update is called once per frame
    void Update()
    {
        if (healthAmount >= 3)
        {
            heart3A.SetActive(true);
            heart3B.SetActive(false);
        }
        else
        {
            heart3A.SetActive(false);
            heart3B.SetActive(true);
        }
        if (healthAmount >= 2)
        {
            heart2A.SetActive(true);
            heart2B.SetActive(false);
        }
        else
        {
            heart2A.SetActive(false);
            heart2B.SetActive(true);
        }
        if (healthAmount >= 1)
        {
            heart1A.SetActive(true);
            heart1B.SetActive(false);
        }
        else
        {
            heart1A.SetActive(false);
            heart1B.SetActive(true);
        }
    }
}
