using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatScript : MonoBehaviour
{
    public int ItemID;
  
    MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
     


                if (ShopStore.ShopStorage.shopItems[4, ItemID] == 1)
                {
                    mr.enabled = true;

                }
                else if (ShopStore.ShopStorage.shopItems[4, ItemID] == 0)
                {
                    mr.enabled = false;

                }



            }


        }
    