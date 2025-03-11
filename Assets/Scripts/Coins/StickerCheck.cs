using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerCheck : MonoBehaviour
{

    [SerializeField] int LevelNum;
    Image imageComponent;
    // Start is called before the first frame update
    void Awake()
    {

        imageComponent = this.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (StickerStore.StickerStorage != null)
        {
            if (StickerStore.StickerStorage.StickerList[LevelNum] == true)
            {

                imageComponent.enabled = true;
            }
            else
            {

                imageComponent.enabled = false;

            }
        }
        
    }
}
