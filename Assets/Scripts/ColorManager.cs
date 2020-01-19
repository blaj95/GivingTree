using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public DonationManager donationManager;
    public Color CurrentColor;
    public Color brightGreen;
    public Color Teal;
    public Color Gold;
    public Color red;
    public Color purple;
    public List<Image> pickerBGs;
    private int index;
    public void OnBrightGreen()
    {
        CurrentColor = brightGreen;
        donationManager.leafRend1.material.color = CurrentColor;
        donationManager.leafRend2.material.color = CurrentColor;
        index = 0;
        for (int i = 0; i < pickerBGs.Count; i++)
        {
            if (i == index)
            {
                pickerBGs[i].enabled = true;
            }
            else
            {
                pickerBGs[i].enabled = false;
            }
        }
    }
    
    public void OnTeal()
    {
        CurrentColor = Teal;
        donationManager.leafRend1.material.color = CurrentColor;
        donationManager.leafRend2.material.color = CurrentColor;
        index = 1;
        for (int i = 0; i < pickerBGs.Count; i++)
        {
            if (i == index)
            {
                pickerBGs[i].enabled = true;
            }
            else
            {
                pickerBGs[i].enabled = false;
            }
        }
    }
    
    public void OnGold()
    {
        CurrentColor = Gold;
        donationManager.leafRend1.material.color = CurrentColor;
        donationManager.leafRend2.material.color = CurrentColor;
        index = 2;
        for (int i = 0; i < pickerBGs.Count; i++)
        {
            if (i == index)
            {
                pickerBGs[i].enabled = true;
            }
            else
            {
                pickerBGs[i].enabled = false;
            }
        }
    }
    
    public void OnRed()
    {
        CurrentColor = red;
        donationManager.leafRend1.material.color = CurrentColor;
        donationManager.leafRend2.material.color = CurrentColor;
        index = 3;
        for (int i = 0; i < pickerBGs.Count; i++)
        {
            if (i == index)
            {
                pickerBGs[i].enabled = true;
            }
            else
            {
                pickerBGs[i].enabled = false;
            }
        }

    }
    
    public void OnPurple()
    {
        CurrentColor = purple;
        donationManager.leafRend1.material.color = CurrentColor;
        donationManager.leafRend2.material.color = CurrentColor;
        index = 4;
        for (int i = 0; i < pickerBGs.Count; i++)
        {
            if (i == index)
            {
                pickerBGs[i].enabled = true;
            }
            else
            {
                pickerBGs[i].enabled = false;
            }
        }

    }
}
