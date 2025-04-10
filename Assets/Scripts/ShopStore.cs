using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShopStore : MonoBehaviour
{
    public static ShopStore ShopStorage;
    public int[,] shopItems = new int[5, 5];



    void Awake()
    {

        if (ShopStorage != null)
        {
            Destroy(gameObject);
        }
        else
        {

            ShopStorage = this;
        }





        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //IDS
        shopItems[1,1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //PRICES
        shopItems[2, 1] = 30;
        shopItems[2, 2] = 40;
        shopItems[2, 3] = 50;
        shopItems[2, 4] = 100;

        //PURCHASED 0 or 1
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;

        //EQUIPED 0 or 1
        shopItems[4, 1] = 0;
        shopItems[4, 2] = 0;
        shopItems[4, 3] = 0;
        shopItems[4, 4] = 0;


    }


    public void BuyEquip()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (CoinStore.CoinStorage.TotalCoinValue >= shopItems[2, ButtonRef.GetComponent<ShopItem>().ItemID] && shopItems[3, ButtonRef.GetComponent<ShopItem>().ItemID] != 1)
        {
            CoinStore.CoinStorage.TotalCoinValue -= shopItems[2, ButtonRef.GetComponent<ShopItem>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ShopItem>().ItemID]++;
            shopItems[4, ButtonRef.GetComponent<ShopItem>().ItemID]++;
        }

        if(shopItems[3, ButtonRef.GetComponent<ShopItem>().ItemID] == 1 && shopItems[4, ButtonRef.GetComponent<ShopItem>().ItemID] == 1)
        {
            Debug.Log("Unequip");
            shopItems[4, ButtonRef.GetComponent<ShopItem>().ItemID] = 0 ;
        }
        else if (shopItems[3, ButtonRef.GetComponent<ShopItem>().ItemID] == 1 && shopItems[4, ButtonRef.GetComponent<ShopItem>().ItemID] == 0)
        {
            Debug.Log("Equip");
            shopItems[4, ButtonRef.GetComponent<ShopItem>().ItemID] = 1;
        }

    }
}
