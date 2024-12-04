using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch=UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.EventSystems;


[RequireComponent(typeof(ARRaycastManager),typeof(ARPlaneManager))]
public class PlaceObjectOnPlane : MonoBehaviour
{
    [Header("References for Spawn")]
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private RingManager ring;
    [Header("GameManager Reference")]
    [SerializeField]
    private GameManager gm;
    [Header("Game Number of Rings")]
    [SerializeField]
    private int nbRings=2;
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        aRRaycastManager= this.GetComponent<ARRaycastManager>();
        aRPlaneManager = this.GetComponent<ARPlaneManager>();
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if(finger.index != 0)
        {
            return;
        }
        Vector2 touch = finger.currentTouch.screenPosition;
        if (aRRaycastManager.Raycast(touch, hits, TrackableType.PlaneWithinPolygon)) 
        {
            Pose pose = hits[0].pose;
            ARPlane plane = aRPlaneManager.GetPlane(hits[0].trackableId);
            BoxCollider box=plane.gameObject.AddComponent<BoxCollider>();
            box.size = new Vector3(plane.size.x, 0.001f, plane.size.y);
            gm.setGamePlan(plane);
            Vector3 spawnPos = new Vector3(pose.position.x, pose.position.y + 0.01f, pose.position.z);
            ring.spawnRings(nbRings, spawnPos, plane.extents.x/2, plane.extents.y/2);
            GameObject obj = Instantiate(prefab, spawnPos, pose.rotation);

        }
    }
    private void OnDestroy()
    {
        
    }
    private void OnDisable()
    {
        
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }
}

