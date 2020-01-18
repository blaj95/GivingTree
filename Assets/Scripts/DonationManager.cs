using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DonationManager : MonoBehaviour
{
    public GameObject donationUI, confirmUI, leafPrefab, treeColliders;
    public Transform arCamera;
    public TMP_InputField amountInput, nameInput, messageInput;
    public TMP_Text donationResponse;
    private PlaceableLeaf leafPlacer;
    private Transform closestLeafPoint;
    public List<Transform> leafPoints;
    public bool placeLeaf, spawnedLeaf;
    public float lastDistance;
    private Donation currentDonation;
    
    // Update is called once per frame
    void Update()
    {
        if (!spawnedLeaf)
        {
            if (Input.touchCount > 0)
            {
                Ray raycastTap = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycastTap, out raycastHit))
                {
                    ObjectInteractions(raycastHit.transform.tag);
                }   
            }   
        }

        //READY TO PLACE LEAF AFTER SUBMITING DONATION INFO
        else if (placeLeaf)
        {
            if (Input.touchCount > 0)
            {
                if (Input.touchCount > 0)
                {
                    Ray raycastTap = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(raycastTap, out raycastHit))
                    {
                        if (raycastHit.transform.CompareTag("Tree"))
                        {
                            leafPlacer.MoveToTree(GetClosestPoint(raycastHit.point));
                            confirmUI.SetActive(true);
                        }
                    }   
                }
            }
        }
    }
    
    void ObjectInteractions(string tag)
    {
        switch (tag)
        {
            case "Donate":
                donationUI.SetActive(true);
                GameObject newLeaf = Instantiate(leafPrefab, arCamera.position, arCamera.rotation, arCamera);
                leafPlacer = newLeaf.GetComponent<PlaceableLeaf>();
                spawnedLeaf = true;
                break;
        }
    }

    public void SubmitDonation()
    {
        Debug.Log("Submit Donation");
        Donation newDonation = new Donation(float.Parse(amountInput.text), nameInput.text, messageInput.text);
        currentDonation = newDonation;
        donationUI.SetActive(false);
        donationResponse.text = "Place your leaf on the tree!";
        treeColliders.SetActive(true);
        placeLeaf = true;
    }

    public void ConfirmPlacement()
    {
        leafPlacer = null;
        donationResponse.text =
            "Thank you " + currentDonation.name + " for your donation of $" + currentDonation.amount;
        spawnedLeaf = false;
    }

    public Transform GetClosestPoint(Vector3 hitPose)
    {
        for (int i = 0; i < leafPoints.Count; i++)
        {
            float distance = Vector3.Distance(leafPoints[i].position, hitPose);
            if (distance < lastDistance)
            {
                Debug.Log("Found Closer Leaf!");
                closestLeafPoint = leafPoints[i];  
                lastDistance = distance;
            }
        }
      
        lastDistance = 100;
        return closestLeafPoint;
    }
}

public class Donation
{
    public float amount;
    public string name;
    public string message;
    
    public Donation(float _amount, string _name, string _message)
    {
        amount = _amount;
        name = _name;
        message = _message;
    }
}
