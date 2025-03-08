using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//This script is currently not being used as I changed tactic however worth keeping as may be used later.
/* Was originally used to change the y damping effectivly allowing the player to see more below them when falling however the camera was 2 snappy or 2 slow so used a differnt method CJ*/
//Please not script was created based on https://youtu.be/9dzBrLUIF8g?si=c4IeDu9rmtu3wxHa
public class VerticalCamManager : MonoBehaviour
{
    public static VerticalCamManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;

    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 1f;
    public float _fallSpeedYDampingChangeThreshold = -5f;

    public bool IsLerpingYDamping { get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _lerpYPanCoroutine;
    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private float _normYPanAmount = 2f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }

        //Gives us the ability to switch between diferent cameras while playing. Currently not needed but may be useful later CJ
        for (int i = 0; i < _allVirtualCameras.Length; i++)
        {
            if (_allVirtualCameras[i].enabled)
            {
                // Set the current active camera
                _currentCamera = _allVirtualCameras[i];

                // Set the framing transposer
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
    }
   

    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
        
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        // Grab the starting damping amount
        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        // Determine the end damping amount
        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = _normYPanAmount;
        }

        // Lerp the pan amount
        float elapsedTime = 0f;
        while (elapsedTime < _fallYPanTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / _fallYPanTime));
            _framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }

        IsLerpingYDamping = false;
    }





}

