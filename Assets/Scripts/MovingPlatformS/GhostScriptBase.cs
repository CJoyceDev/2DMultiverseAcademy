using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScriptBase : MonoBehaviour
{
    GhostScriptLeft ghostScriptLeft;
    GhostsCriptRight ghostScriptRight;
    public Vector3 basePosLeft;
    public Vector3 basePosRight;



    // Start is called before the first frame update
    void LateStart()
    {
        ghostScriptLeft = GetComponentInChildren<GhostScriptLeft>();
        ghostScriptRight = GetComponentInChildren<GhostsCriptRight>();
       
        
        basePosLeft = ghostScriptLeft.ghostPosLeft;
        basePosRight = ghostScriptRight.ghostPosRight;

        print(basePosLeft);
        print(basePosRight);

    }
}
