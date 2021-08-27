using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private float zoomSpeed;
    [SerializeField]
    private float zoomInMax;
    [SerializeField]
    private float zoomOutMax;


    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.UpArrow) && virtualCamera.m_Lens.OrthographicSize > zoomInMax)
            virtualCamera.m_Lens.OrthographicSize -= zoomSpeed * Time.deltaTime;

        if(Input.GetKey(KeyCode.DownArrow) && virtualCamera.m_Lens.OrthographicSize < zoomOutMax)
            virtualCamera.m_Lens.OrthographicSize += zoomSpeed * Time.deltaTime;
    }

}
