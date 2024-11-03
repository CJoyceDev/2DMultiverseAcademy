using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinBehaviour : MonoBehaviour
{
    
    public static CoinBehaviour instance;

    public TMP_Text Counter;
    public int CoinAmount;
    
    void Awake()
    {
        instance = this;

    }
    // sets the starting coin amount to 0
    void Start()
    {
        CoinAmount = 0;
        Counter.text = "Coins:" + CoinAmount.ToString();
     // Coinbody.tag = "CoinCollect";
    }
    //Adds the coin value to the counter CD
    public void AddCoin(int Add)
    {
        CoinAmount += Add;
        Counter.text = "Coins:" + CoinAmount.ToString();

    }
}
