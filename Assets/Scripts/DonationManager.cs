using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DonationManager : MonoBehaviour
{
    public GameObject donationUI, confirmUI, editUI, leafPrefab, treeColliders;
    public Transform arCamera;
    public TMP_InputField amountInput, nameInput, messageInput;
    public TMP_Text donationResponse;
    private PlaceableLeaf leafPlacer;
    private Transform closestLeafPoint;
    public List<Transform> leafPoints;
    public bool placeLeaf, spawnedLeaf;
    public float lastDistance;
    private Donation currentDonation;
    public DonationMeter donationMeter;
    private ParticleSystem leafParticles;
    public int uiLayer;
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
                Ray raycastTap = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycastTap, out raycastHit))
                {
                    if (raycastHit.transform.gameObject.layer == 1<<uiLayer)
                    {
                        Debug.Log("Touched UI");
                        return;
                    }
                    
                    if (raycastHit.transform.CompareTag("Tree"))
                    {
                        leafPlacer.MoveToTree(GetClosestPoint(raycastHit.point));
                        confirmUI.SetActive(true);
                        
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
                donationResponse.text = "";
                donationUI.SetActive(true);
                GameObject newLeaf = Instantiate(leafPrefab, arCamera.position, arCamera.rotation, arCamera);
                leafPlacer = newLeaf.GetComponentInChildren<PlaceableLeaf>();
                // leafParticles = newLeaf.GetComponentInChildren<ParticleSystem>();
                // leafParticles.Stop();
                spawnedLeaf = true;
                break;
            case "Leaf":
                donationResponse.text = "Your placed leaf";
                break;
            case "OtherLeaf":
                donationResponse.text = "Another Leaf";
                break;
        }
    }

    public void SubmitDonation()
    {
        Debug.Log("Submit Donation");
        donationUI.SetActive(false);
        donationResponse.text = "Place your leaf on the tree!";
        treeColliders.SetActive(true);
        if(placeLeaf)
            confirmUI.SetActive(true);
        // editUI.SetActive(true);
        placeLeaf = true;
        // leafParticles.Play();
    }

    public void EditDonation()
    {
        donationUI.SetActive(true);
        treeColliders.SetActive(false);
       // editUI.SetActive(false);
    }
    
    public void ConfirmPlacement()
    {
        leafPlacer = null;
        Donation newDonation = new Donation(float.Parse(amountInput.text), nameInput.text, messageInput.text);
        currentDonation = newDonation;
        donationResponse.text =
            "Thank you " + currentDonation.name + " for your donation of $" + currentDonation.amount;
        donationMeter.OnDonation(currentDonation.amount);
        spawnedLeaf = false;
        confirmUI.SetActive(false);
        // editUI.SetActive(false);
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
