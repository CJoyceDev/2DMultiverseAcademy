using System.Collections;
using UnityEngine;

//Please not script was created based on https://youtu.be/9dzBrLUIF8g?si=c4IeDu9rmtu3wxHa however a lot has been adapted to work for our project. CJ

/// <WhatThisIs>
/// This script is used to move an empty object around the scene/character. This empty object object
/// is what the Camera is attached to. This means we can change the camera to show more of the level around the player as required. 
/// E.G. Show more in front of the camera CJ
/// </WhatThisIs>
public class CamFollowManager : MonoBehaviour
{
    
    [SerializeField] private Transform _playerTransform;

    //Time taken to move between points CJ
    [SerializeField] private float _flipRotationTime = 0.5f;

    private Coroutine _turnCoroutine;
    private Coroutine verticalCoroutine;
    private Coroutine winCoroutine;
    private CJMovementWithRB _player;
    public bool facingRight;
    Vector3 posOffset;
    float offsetValueX;
    [SerializeField]float offsetValueY;
    public float currentYOffset;
    public float winOffset;
    public bool inWinZone;
    bool justWon = false;
    Vector3 winPos;

    public Transform targetTransform;

    private void Awake()
    {
        _player = _playerTransform.gameObject.GetComponent<CJMovementWithRB>();
        facingRight = _player.facingRight;
        inWinZone = false;
    }

    private void Update()
    {
      
        //Moves to the desired point around the player
        posOffset = new Vector3(offsetValueX,offsetValueY,0);
        if (!inWinZone)
        {
            transform.position = _playerTransform.position + posOffset;
            winPos = _playerTransform.position;
        }
        
        if (inWinZone)
        {
           
            transform.position = targetTransform.position + posOffset;
            justWon = true;
            //transform.position = transform.position + new Vector3(offsetValueX,0,0);
            CallWin();
            
            
        }
    }

    public void CallTurn()
    {
        if (_turnCoroutine != null && inWinZone == false)
        {
            StopCoroutine(_turnCoroutine);
        }
        _turnCoroutine = StartCoroutine(ChangePos());
    }

    public void CallLookDown()
    {
        if (verticalCoroutine != null)
        {
            StopCoroutine(verticalCoroutine);
        }
        verticalCoroutine = StartCoroutine(ChangeVerticalPos());
    }

    public void CallWin()
    {
        if(winCoroutine != null )
        {
            StopCoroutine(winCoroutine);
        }
        verticalCoroutine = StartCoroutine(WinPos());
    }





    private IEnumerator ChangePos()
    {
        //Smoothly moves the camera between the start and end position CJ
        //The end position is determined based on what direction the player is facing CJ
        float startOffset = offsetValueX;
        float endOffset = DetermineEndX();

        float elapsedTime = 0f;
        while (elapsedTime < _flipRotationTime)
        {
            elapsedTime += Time.deltaTime;

            
            offsetValueX = Mathf.Lerp(startOffset, endOffset, elapsedTime / _flipRotationTime);
            //print(offsetValueX);
            yield return null;
        }

        
        offsetValueX = endOffset;
    }



    private float DetermineEndX()
    {
        facingRight = !facingRight;
        if (facingRight)
        {
            float newX = 3;
            return newX;
        }
        else if (!facingRight)
        {
            float newX = -3;
            return newX;
        }
        return 0f;
      
    }
    //Changes the vertical pos of cam based on value set in player controller CJ
    private IEnumerator ChangeVerticalPos()
    {
        if (!inWinZone)
        { float startOffset = offsetValueY;
            float endOffset = currentYOffset;

            float elapsedTime = 0f;
            while (elapsedTime < _flipRotationTime)
            {
                elapsedTime += Time.deltaTime;


                offsetValueY = Mathf.Lerp(startOffset, endOffset, elapsedTime / _flipRotationTime);

                yield return null;
            }


            offsetValueY = endOffset;
        }
    }

    private IEnumerator WinPos()
    {
        float startOffset = offsetValueX;
        float endOffset = winOffset;

        float elapsedTime = 0f;
        while (elapsedTime < _flipRotationTime)
        {
            elapsedTime += Time.deltaTime;


            offsetValueX = Mathf.Lerp(startOffset, endOffset, elapsedTime / _flipRotationTime);

            yield return null;
        }


        offsetValueX = endOffset;
        
    }




}
