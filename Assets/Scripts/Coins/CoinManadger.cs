
using UnityEngine;

public class CoinManadger : MonoBehaviour
{
    private void Awake()
    {
        if (CoinsScript.collectedCoins.Count == 0)
        {
            CoinsScript.collectedCoins.Add(0);
            var x = GameObject.FindObjectsOfType<CoinsScript>();
            foreach (var item in x)
            {
                CoinsScript.collectedCoins.Add(item.coinID);
            }
            CoinsScript.collectedSaved = CoinsScript.collectedCoins;
        }
    }
}
