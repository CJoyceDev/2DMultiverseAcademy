using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerStore : MonoBehaviour
{
    public static StickerStore StickerStorage;
    public bool[] StickerList = new bool[18];
    [SerializeField] GameObject[] Stickers = new GameObject[18];

    void Awake()
    {
        StickerStorage = this;
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i <= 17; i++)
        { StickerList[i] = false; }

    }

    void LevelCheck()
    {

        for(int i = 0; i <= 17; i++)
        {
            if(StickerList[i])
            {

                Stickers[i].SetActive(true);

            }

        }

     }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
