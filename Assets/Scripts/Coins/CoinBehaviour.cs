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
    // sets the starting coin amount to 0 CD
    void Start()
    {
        CoinAmount = 0;
        if (Counter != null)
        {
            Counter.text = "X " + CoinAmount.ToString();
        }
        
     // Coinbody.tag = "CoinCollect";
    }
    //Adds the coin value to the counter CD
    public void AddCoin(int Add)
    {
        if (Counter != null)
        {
            CoinAmount += Add;
            CoinStore.CoinStorage.LevelCoinValue = CoinAmount;
            Counter.text = "X " + CoinAmount.ToString();
        }

    }
}
