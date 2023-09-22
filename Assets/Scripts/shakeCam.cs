using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeCam : MonoBehaviour
{
    private static shakeCam instance;
    public static shakeCam Instance { get { return instance; } }


    private CinemachineVirtualCamera CinemachineVirtualCamera;
    private float shakeSensivity = 1f;
    private float shakeTime = 0.2f;
    private float timer;
    private CinemachineBasicMultiChannelPerlin cbmcp;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        stopShake();
    }

    public void shakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = shakeSensivity;

        timer = shakeTime;
    }

    public void stopShake()
    {
        CinemachineBasicMultiChannelPerlin cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = 0f;
        timer = 0f;
    }

    private void Update()
    {
        if(timer > 0f)
        {
            timer -=Time.deltaTime;

            if(timer <= 0f) 
            {
                stopShake();
            }
        }
    }
}
