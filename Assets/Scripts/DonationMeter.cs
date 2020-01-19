using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DonationMeter : MonoBehaviour
{
    public Slider meterHandle;
    public Image donationMeter;
    public TMP_Text currentDonationTotalText;
    public Animator uiPop;
    public float currentDonationTotal;
    public float testDonation;
    public GameObject goalReachedText;
    public bool pressed;

    public FlowerGenerator ground;
    public FlowerGenerator tree;

    public GameObject normalMusic;
    public GameObject fundedMusic;
    
    // Start is called before the first frame update
    void Update()
    {
        currentDonationTotalText.text = "$" + currentDonationTotal;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDonation(testDonation);
        }
    }

    // Update is called once per frame
    public void OnDonation(float donation)
    {
        currentDonationTotal += donation;
        meterHandle.value = currentDonationTotal;
        
        //Goal reached
        if (currentDonationTotal >= 30000)
        {
            Debug.Log("End Goal Reached");
            goalReachedText.SetActive(true);
            tree.NextBloom();
            ground.NextBloom();
            normalMusic.SetActive(false);
            fundedMusic.SetActive(true);
        }
    }

    public void OnSliderChanged()
    {
        donationMeter.fillAmount = currentDonationTotal / 30000;
    }

    public void UIPop()
    {
        if (!pressed)
        {
            uiPop.SetTrigger("Up");
            pressed = true;
        }
        else
        {
            uiPop.SetTrigger("Down");
            pressed = false;
        }
    }
}
