using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DonationManager : MonoBehaviour
{
    public GameObject donationUI, confirmUI, editUI, leafPrefab, treeColliders, donationBar, colorPicker, donationReadPanel;
    public GameObject currentLeaf;
    public Transform arCamera;
    public TMP_InputField amountInput, nameInput, messageInput;
    public TMP_Text donationResponse, placedDonationName, placedDonationMessage;
    private PlaceableLeaf leafPlacer;
    private Transform closestLeafPoint;
    public List<Transform> leafPoints;
    public bool placeLeaf, spawnedLeaf, colorPick;
    public float lastDistance;
    private Donation currentDonation;
    public Donations donations;
    public DonationMeter donationMeter;
    private ParticleSystem leafParticles;
    public MeshRenderer leafRend1;
    public MeshRenderer leafRend2;
    public ColorManager colorManager;
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
                    ObjectInteractions(raycastHit.transform.tag, raycastHit.transform.gameObject);
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
    
    void ObjectInteractions(string tag, GameObject hitObject)
    {
        switch (tag)
        {
            case "Donate":
                donationResponse.text = "";
                donationUI.SetActive(true);
                GameObject newLeaf = Instantiate(leafPrefab, arCamera.position, arCamera.rotation, arCamera);
                leafPlacer = newLeaf.GetComponentInChildren<PlaceableLeaf>();
                currentLeaf = newLeaf;
                // leafParticles = newLeaf.GetComponentInChildren<ParticleSystem>();
                // leafParticles.Stop();
                spawnedLeaf = true;
                break;
            case "Leaf":
                donationResponse.text = "Your placed leaf";
                break;
            case "OtherLeaf":
                string[] leafNum = hitObject.name.Split('_');
                donationResponse.text = "Another Leaf " + leafNum[1];
                placedDonationName.text = donations.donations[int.Parse(leafNum[1])].name;
                placedDonationMessage.text = donations.donations[int.Parse(leafNum[1])].message;
                donationReadPanel.SetActive(true);
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
        placeLeaf = false;
        Donation newDonation = new Donation(float.Parse(amountInput.text), nameInput.text, messageInput.text);
        currentDonation = newDonation;
        if (donations.donations.Contains(currentDonation))
        {
            donations.donations.Add(currentDonation);    
        }
        
        if (colorPick)
        {
            // SET LEAF TO SELECTED COLOR
            confirmUI.SetActive(false);
            donationResponse.text =
                "Thank you " + currentDonation.name + " for your premium level donation of $" + currentDonation.amount;
            donationMeter.OnDonation(currentDonation.amount);
            spawnedLeaf = false;
            ColorPick();
            currentLeaf = null;
            return;
        }

        if (currentDonation.amount >= 50)
        {
            spawnedLeaf = false;
            ColorPick();
        }
        else
        {
            confirmUI.SetActive(false);
            donationResponse.text =
                "Thank you " + currentDonation.name + " for your donation of $" + currentDonation.amount;
            donationMeter.OnDonation(currentDonation.amount);
            spawnedLeaf = false;
            currentLeaf = null;
            // editUI.SetActive(false);   
        }
    }

    public void ColorPick()
    {
        if (!colorPick)
        {
            donationBar.SetActive(false);
            colorPicker.SetActive(true);   
            leafRend1 = currentLeaf.GetComponent<PlaceableLeaf>().rend1;
            leafRend2 = currentLeaf.GetComponent<PlaceableLeaf>().rend2;
            colorPick = true;
        }
        else
        {
            donationBar.SetActive(true);
            colorPicker.SetActive(false);   
            colorPick = false;
        }
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
