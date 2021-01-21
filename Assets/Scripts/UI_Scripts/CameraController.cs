using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed;
    public float distance = 5.0f;
    public float maxDistance = 20;
    public float minDistance = .6f;
    public int zoomRate = 40;
    private Transform target;
    private float desiredDistance;
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;

    private bool camBlocked;
    
    void Start() { Init(); }
    void OnEnable() { Init(); }
    
    public void Init()
    {
        GameObject go = GameObject.Find("Cam Target");
        target = go.transform;
        transform.LookAt(target.position);
        Vector3 position = - distance * Vector3.back + target.position;
        transform.position = position;
        
        desiredDistance = distance;

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
        if (Input.mouseScrollDelta.y != 0 )
        {
            desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
            desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);

            // calculate position based on the new currentDistance
            Vector3 position = desiredDistance * (transform.position - target.position).normalized + target.position;
            transform.position = position;
        }
        // If middle mouse and left alt are selected? ORBIT
        else if (Input.GetMouseButton(0))
        {          
            xDeg += Input.GetAxis("Mouse X") * rotationSpeed;
            yDeg -= Input.GetAxis("Mouse Y") * rotationSpeed;

            Quaternion quat = Quaternion.Euler(yDeg, xDeg, 0.0f);
            transform.position = quat * new Vector3(0.0f, 0.0f, -desiredDistance) + target.position;
            transform.rotation = quat;
        }
    }
}
