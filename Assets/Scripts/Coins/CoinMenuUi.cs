using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinMenuUi : MonoBehaviour
{

    public TMP_Text Counter;

    // Start is called before the first frame update
    void Start()
    {

        Counter = GetComponent<TMP_Text>();

        if (Counter != null)
        {
            Counter.text = "X " + CoinStore.CoinStorage.TotalCoinValue.ToString();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
