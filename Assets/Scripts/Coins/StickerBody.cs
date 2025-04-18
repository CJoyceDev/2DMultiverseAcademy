using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickerBody : MonoBehaviour
{

    [SerializeField] int LevelNum;

    [SerializeField] AudioClip CollectSound;

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
            if (CollectSound != null)
            {
                SoundHandler.instance.PlaySound(CollectSound, transform, 1f);
            }


            //Put in a null check as null reference exceptions are bad and prevent the game being built CJ
            Debug.Log("Stickerhit");
            if (StickerStore.StickerStorage != null)
            {
                StickerStore.StickerStorage.StickerList[LevelNum] = true;
            }
            this.gameObject.SetActive(false);
        }



    }

}
