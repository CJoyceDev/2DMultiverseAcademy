using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoreButton : MonoBehaviour
{
    public Button Button;


    void Awake()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        //Button.onClick.AddListener(TaskOnClick);
        
    }

    void TaskOnClick()
    {
        
        ShopStore.ShopStorage.BuyEquip();

    }


}
