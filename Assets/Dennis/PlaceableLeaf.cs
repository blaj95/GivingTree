using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableLeaf : MonoBehaviour
{
	public bool unlocked;
	public GameObject ground;

    public float moveToUserDuration = 2f;
    public float moveToTreeDuration = 2f;

    private Transform arCamera;
    private bool movingToUser;
    private float movingToUserTime;

    private bool heldByUser;

    private bool movingToTree;
    private float movingToTreeTime;
    private Transform placedTransform;

    private Vector3 heldByUserPos;

    // Start is called before the first frame update
    void Start()
    {
        arCamera = GameObject.Find("ARCamera").GetComponent<Transform>();
        ground = GameObject.Find("GroundPrefab");
        heldByUserPos = new Vector3(0f, -.085f, 0.205f);
        movingToUser = false;
        heldByUser = false;
        movingToTree = false;

        //GenerateOnGround();
    }

    // Update is called once per frame
    void Update()
    {
        // if (movingToUser)
        // {
        //     if(Time.time - movingToUserTime < moveToUserDuration ){
        //         transform.position = Vector3.Lerp(transform.position, arCamera.position + heldByUserPos, (Time.time - movingToUserTime) / moveToUserDuration);
        //     }
        //     else
        //     {
        //         movingToUser = false;
        //         heldByUser = true;
        //     }
        // }
        // else if (heldByUser)
        // {
        //     // transform.position = arCamera.position + heldByUserPos;
        // }
        // else
        if (movingToTree)
        {
            if (Time.time - movingToTreeTime < moveToTreeDuration)
            {
                transform.position = Vector3.Lerp(transform.position, placedTransform.position, (Time.time - movingToTreeTime) / moveToTreeDuration);
            }
            else
            {
                movingToTree = false;
                //LEAF HAS OFFICIALLY BEEN PLACED ON TREE HERE
            }
        }
                
    }

   

    // private void OnMouseDown()
    // {
    //     if (!movingToUser)
    //     {
    //         movingToUser = true;
    //         movingToUserTime = Time.time;
    //     }
    // }

    public void MoveToTree(Transform t)
    {
        transform.parent = null;
        placedTransform = t;
        movingToTree = true;
        movingToTreeTime = Time.time;
    }


    // private void GenerateOnGround()
    // {
    //     Vector3 userDirectionFromGround = (ground.transform.position - arCamera.position).normalized;
    //     // transform.position = arCamera.position + userDirectionFromGround * 3f;
    //     // transform.LookAt(arCamera);
    // }
}
