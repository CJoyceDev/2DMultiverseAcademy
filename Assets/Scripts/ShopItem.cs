using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    // Start is called before the first frame update
    public int ItemID;
    public TMP_Text Price;
    public GameObject ShopStore;

    // Update is called once per frame
    void Update()
    {
        if (ShopStore.GetComponent<ShopStore>().shopItems[3, ItemID] == 0)
        {
            Price.text = "Coins x " + ShopStore.GetComponent<ShopStore>().shopItems[2, ItemID].ToString();
        }
        else if (ShopStore.GetComponent<ShopStore>().shopItems[4, ItemID] == 1)
        {
            Price.text = "Equiped";

        }
        else if (ShopStore.GetComponent<ShopStore>().shopItems[4, ItemID] == 0)
        {
            Price.text = " ";

        }
    }
}
