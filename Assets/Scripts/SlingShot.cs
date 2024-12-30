using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlingShot : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler
{
    [SerializeField] 
    private LayerMask targetLayers;
    
    private Vector3 startPosition;
    private Vector3 pullPosition;
    
    private Camera MainCamera;
    private Camera SubCamera;
    
    public static float PowerMultiplier = 10;
 
    public LineRenderer lineRenderer;
    [SerializeField] private float maxPullDistance;

    public Action OnFly;
    public Action OnDestroyed;
    
    private void Awake()
    {
        MainCamera = Camera.main;
    }
    
    private void OnApplicationQuit()
    {
        OnDestroyed = null;
        OnFly = null;
    }

    private void OnDestroy()
    {
        if (!OnDestroyed.IsUnityNull())
            OnDestroyed?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pullPosition = startPosition = MainCamera.ScreenToWorldPoint(
            new Vector3(eventData.position.x, 
                eventData.position.y , 
                MainCamera.WorldToScreenPoint(transform.position).z));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MainCamera.enabled = false;
        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 endPos = lineRenderer.GetPosition(1);

        Vector3 current_Force = startPos - endPos;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(current_Force * PowerMultiplier, ForceMode.Impulse);
        
        OnFly?.Invoke();
        
        Invoke("cameraOn", 3.0f);
        Destroy(this.gameObject, 3.0f);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Camera.main != null)
        {
            Vector3 mouseWorldPos = MainCamera.ScreenToWorldPoint(
                new Vector3(eventData.position.x, 
                eventData.position.y , 
                MainCamera.WorldToScreenPoint(transform.position).z));
            
            Vector3 pullDirection = startPosition - mouseWorldPos;

            Vector3 LinePosition1 = Vector3.zero;
            
            if (pullDirection.magnitude > maxPullDistance)
            {
                pullDirection = pullDirection.normalized * maxPullDistance;
                LinePosition1 = startPosition - pullDirection;
            }
            else
            {
                LinePosition1 = mouseWorldPos;
            }
            
            transform.position = mouseWorldPos;
            
            lineRenderer.SetPosition(1, LinePosition1);
        }
    }

    private void cameraOn()
    {
        MainCamera.enabled = true;
    }


}
