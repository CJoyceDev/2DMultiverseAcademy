using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerBody : MonoBehaviour
{

    [SerializeField] int LevelNum;


    void Start()
    {
        if (StickerStore.StickerStorage != null)
        {
            if (StickerStore.StickerStorage.StickerList[LevelNum])
            {
                this.gameObject.SetActive(false);

            }
        }
        

    }


    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Stickerhit");
            StickerStore.StickerStorage.StickerList[LevelNum] = true;
            this.gameObject.SetActive(false);
        }



    }

}
