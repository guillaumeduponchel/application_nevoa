﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float maxDistance = 20;
    public float minDistance = .6f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public int zoomRate = 40;
    public float panSpeed = 0.3f;
    
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float desiredDistance;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

    private bool camBlocked;
    
    void Start() { Init(); }
    void OnEnable() { Init(); }
    
    public void Init()
    {
        //If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }
        
        distance = Vector3.Distance(transform.position, target.position);
        desiredDistance = distance;
        
        //be sure to grab the current rotations as starting points.
        position = transform.position;
        rotation = transform.rotation;
        desiredRotation = transform.rotation;
        
        xDeg = Vector3.Angle(Vector3.right, transform.right );
        yDeg = Vector3.Angle(Vector3.up, transform.up );
    }
    
    /*
    * Camera logic on LateUpdate to only update after all character movement logic has been handled.
    */
    void LateUpdate()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        // If Control and Alt and Middle button? ZOOM!
        if (Input.GetMouseButton(2) && Input.mouseScrollDelta.y != 0 )
        {
            desiredDistance -= Input.GetAxis("Mouse Y") * Time.deltaTime * zoomRate*0.125f * Mathf.Abs(desiredDistance);
        }
        // If middle mouse and left alt are selected? ORBIT
        else if (Input.GetMouseButton(0))
        {
            xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            
            ////////OrbitAngle
            
            //Clamp the vertical axis for the orbit
            yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
            // set camera rotation
            desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);           
            transform.rotation = desiredRotation;
        }
        
        ////////Orbit Position
        
        // affect the desired Zoom distance if we roll the scrollwheel
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        //clamp the zoom min/max
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        
        // calculate position based on the new currentDistance
        position = target.position - (rotation * Vector3.forward * desiredDistance + targetOffset);
        transform.position = position;
    }
       
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}

