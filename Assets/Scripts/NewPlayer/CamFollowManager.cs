using System.Collections;
using UnityEngine;

public class CamFollowManager : MonoBehaviour
{
    
    [SerializeField] private Transform _playerTransform;

    
    [SerializeField] private float _flipRotationTime = 0.5f;

    private Coroutine _turnCoroutine;
    private CJMovementWithRB _player;
    private bool facingRight;
    Vector3 posOffset;
    float offsetValue;

    private void Awake()
    {
        _player = _playerTransform.gameObject.GetComponent<CJMovementWithRB>();
        facingRight = _player.facingRight;
    }

    private void Update()
    {
       
        posOffset = new Vector3(offsetValue,0,0);
        transform.position = _playerTransform.position + posOffset;
    }

    public void CallTurn()
    {
        if (_turnCoroutine != null)
        {
            StopCoroutine(_turnCoroutine);
        }
        _turnCoroutine = StartCoroutine(ChangePos());
    }

    private IEnumerator FlipLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotationY();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < _flipRotationTime)
        {
            elapsedTime += Time.deltaTime;

            // Lerp the Y rotation
            yRotation = Mathf.Lerp(startRotation, endRotationAmount, elapsedTime / _flipRotationTime);
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private IEnumerator ChangePos()
    {

        float startOffset = offsetValue;
        float endOffset = DetermineEndX();

        float elapsedTime = 0f;
        while (elapsedTime < _flipRotationTime)
        {
            elapsedTime += Time.deltaTime;

            
            offsetValue = Mathf.Lerp(startOffset, endOffset, elapsedTime / _flipRotationTime);
            print(offsetValue);
            yield return null;
        }

        
        offsetValue = endOffset;
    }



    private float DetermineEndRotationY()
    {
        facingRight = !facingRight;

        if (facingRight)
        {
            return 180f;
        }
        else
        {
            return 0f;
        }
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

 
}
