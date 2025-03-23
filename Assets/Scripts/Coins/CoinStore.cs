using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStore : MonoBehaviour
{
    public static CoinStore CoinStorage;
    public float TotalCoinValue;
    public float LevelCoinValue;
    public float AddedLevelCoinValue;
    public float CheckpointCoinValue;


    void Awake()
    {

        if (CoinStorage != null)
        {
            Destroy(gameObject);
        }
        else
        {

            CoinStorage = this;
        }




          
        DontDestroyOnLoad(gameObject);
    }

    //when level ends add levelcoin value to total coin value
    // save level coin value at checkpoints

    public void CheckPointSet()
    {
        CheckpointCoinValue = LevelCoinValue;
    }

    public void CheckPointGet()
    {
        LevelCoinValue = CheckpointCoinValue;

    }

    public void LevelStartSet()
    {
        LevelCoinValue = TotalCoinValue;

    }

    public void LevelEndSet()
    {
        TotalCoinValue = TotalCoinValue + LevelCoinValue;

    }

    public void ShopSpent()
    {


    }
}
