using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsScript : MonoBehaviour
{
    public float rotationSpeed = 100f;

    public static List<int> collectedCoins = new List<int>();
    public static List<int> collectedSaved = new List<int>();
    public int coinID;

    // Update is called once per frame
    //simple script to make coin assets rotate CJ

    private void Start()
    {
        if (collectedSaved.Contains(coinID))
        {
            this.gameObject.SetActive(true);
        }
        else
        {
          //  this.gameObject.SetActive(false);
        }
    }



    void Update()
    {
        float angle = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * angle, Space.World);
    }


     // Commented out to not contradict with other coin code CD
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            collectedCoins.Remove(coinID);
            this.gameObject.SetActive(false);
        }



    }
    
}
