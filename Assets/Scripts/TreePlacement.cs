using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TreePlacement : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject tree;
    public GameObject leafPrefab;
    private Vector2 touchPosition;
    public bool treeSpawned;
    private float lastDistance = 100;
    private Transform closestLeafPoint;
    public List<Transform> leafPoints;
    // Start is called before the first frame update
    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (!treeSpawned)
        {
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
            
                //SPAWN AT HITPOSE
                tree.transform.position = hitPose.position;
                tree.SetActive(true);
                treeSpawned = true;
                return;
            }   
        }
        // else if (treeSpawned)
        // {
        //     Ray raycastTap = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //     RaycastHit raycastHit;
        //     if (Physics.Raycast(raycastTap, out raycastHit))
        //     {
        //         if (raycastHit.transform.CompareTag("Tree"))
        //         {
        //             for (int i = 0; i < leafPoints.Count; i++)
        //             {
        //                 float distance = Vector3.Distance(leafPoints[i].position, raycastHit.point);
        //                 if (distance < lastDistance)
        //                 {
        //                     Debug.Log("Found Closer Leaf!");
        //                     closestLeafPoint = leafPoints[i];  
        //                     lastDistance = distance;
        //                 }
        //             }
        //             GameObject leaf = Instantiate(leafPrefab, closestLeafPoint.position, Quaternion.identity);
        //             lastDistance = 100;
        //             // var direction = (raycastHit.transform.position - leaf.transform.position).normalized;
        //             // leaf.transform.forward = direction;
        //         }
        //     }
        // }
    }

    void ObjectInteractions()
    {
        
    }
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }
}
