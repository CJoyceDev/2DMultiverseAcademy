using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinValues : MonoBehaviour
{
    //lets differnt coins have differnt values CD
    public int Value = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //collision with the player for colection CD
    void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("Player"))
        {

            // Destroys collected coin. CD
            Destroy(gameObject);
            CoinBehaviour.instance.AddCoin(Value);
            /*Debug.Log("coin");*/
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
