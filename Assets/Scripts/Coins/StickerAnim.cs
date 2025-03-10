using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationCurve myCurve;

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), this.transform.position.z);
    }
}
