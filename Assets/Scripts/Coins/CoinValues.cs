using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinValues : MonoBehaviour
{
    //lets differnt cooins have differnt values
    public int Value;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //collision with the player for colection;
    void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("Player"))
        {

            // Destroys collected coin.
            Destroy(gameObject);
            CoinBehaviour.instance.AddCoin(Value);
            Debug.Log("coin");
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
