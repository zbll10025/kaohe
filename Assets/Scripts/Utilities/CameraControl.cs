using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
   [SerializeField] private CinemachineConfiner2D confiner2D;
    public CinemachineImpulseSource impulseSource;
    [Header("监听")]
    public voidEventSO cameraShakeEvent;
    public voidEventSO afterLoadedEvent;
    
    private void OnEnable()
    {
        cameraShakeEvent.OnEventRised += OnCameraShakeEvent;
        afterLoadedEvent.OnEventRised += OnAfterLoadedEvent;
    }
    private void OnDisable()
    {
        cameraShakeEvent.OnEventRised -= OnCameraShakeEvent;
        afterLoadedEvent.OnEventRised -= OnAfterLoadedEvent;
    }

    

    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }
   private void OnAfterLoadedEvent()
    {
        GetNewCameraBounds();
    }
    /// <summary>
    /// 寻找被挂载bound边界的物体
    /// </summary>
    public void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
        {
            Debug.Log("yixunzhao");
            return;
        }                                                          
        confiner2D.m_BoundingShape2D=obj.GetComponentInParent<PolygonCollider2D>();                                                                                                     
        confiner2D.InvalidateCache();
        
    }
}
