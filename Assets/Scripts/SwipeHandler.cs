using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    private Vector3 beginPos;
    private Vector3 endPos;

    public GameObject marker;
    private GameObject beginMarker;
    private GameObject endMarker;
    
    public Camera mainCamera;

    public float swipeThreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (endMarker != null)
                {
                    DeleteMarkers();                  
                }
                Vector3 touchPos = mainCamera.ScreenToWorldPoint(touch.position);
                beginPos = new Vector3(touchPos.x, touchPos.y, mainCamera.nearClipPlane);
                beginMarker = Instantiate(marker, beginPos, Quaternion.identity);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector3 touchPos = mainCamera.ScreenToWorldPoint(touch.position);
                endPos = new Vector3(touchPos.x, touchPos.y, mainCamera.nearClipPlane);
                endMarker = Instantiate(marker, endPos, Quaternion.identity);
                LineRenderer rend = endMarker.AddComponent<LineRenderer>();
                rend.SetPosition(0, endMarker.transform.position);
                rend.SetPosition(1, beginMarker.transform.position);
                CalculateSwipe(beginPos, endPos);
            }
        }
    }

    void CalculateSwipe(Vector3 p1, Vector3 p2)
    {
        float xDiff = p2.x-p1.x;
        float yDiff = p2.y - p1.y;
        Debug.Log(xDiff);
        Debug.Log(yDiff);
        if (Math.Abs(xDiff) > swipeThreshold && Math.Abs(xDiff) > Math.Abs(yDiff))
        {
            if (xDiff > 0)
            {
                Debug.Log("Right");
            }
            else if (xDiff < 0)
            {
                Debug.Log("Left");
            }   
        }
        else if (Math.Abs(yDiff) > swipeThreshold && Math.Abs(yDiff) > Math.Abs(xDiff))
        {
            if (yDiff > 0)
            {
                Debug.Log("Up");
            }
            else if (yDiff < 0)
            {
                Debug.Log("Down");
            }
        }
    }
    void DeleteMarkers()
    {
        Destroy(beginMarker);
        Destroy(endMarker);
    }
}
