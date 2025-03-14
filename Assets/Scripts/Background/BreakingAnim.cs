using System.Collections;
using UnityEngine;

public class BreakingAnim : MonoBehaviour
{
    public CrumblingPlatform crumblingPlatform;
    public GameObject testTubeLeft;
    public GameObject testTubeRight;



    public bool isBreaking = false;
    private float fallSpeed = 18f;
    private float rotationSpeed = 500f;
    public Vector3 startPosRight;
    public Vector3 startPosLeft;

    public Quaternion startRotatinLeft;
    public Quaternion startRotatinRight;

    private void Start()
    {
        startPosLeft = testTubeLeft.transform.position;
        startPosRight = testTubeRight.transform.position;

        startRotatinLeft = testTubeLeft.transform.rotation;
        startRotatinRight = testTubeRight.transform.rotation;
    }

    void Update()
    {
        if (isBreaking)
        {
            MoveAndRotate(testTubeLeft, Vector3.back);
            MoveAndRotate(testTubeRight, Vector3.forward);
        }

    }

    void MoveAndRotate(GameObject obj, Vector3 spinDirection)
    {
        if (obj == null) return;

       
        obj.transform.position += Vector3.down * fallSpeed * Time.deltaTime;


        obj.transform.Rotate(spinDirection * rotationSpeed * Time.deltaTime);
    }

   public void ResetPos(GameObject obj)
    {
        if (obj == testTubeLeft)
        {
            obj.transform.position = startPosLeft;
            obj.transform.rotation = startRotatinLeft;
        }
        else if(obj == testTubeRight)
        {
            obj.transform.position = startPosRight;
            obj.transform.rotation = startRotatinRight;
        }
    }


}
