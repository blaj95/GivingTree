using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public DonationManager donationManager;
    public Color CurrentColor;
    public Color brightGreen;
    public Color Teal;
    public Color Gold;
    public Color red;
    public Color purple;

    public void OnBrightGreen()
    {
        CurrentColor = brightGreen;
        donationManager.leafRend1.material.color = CurrentColor;
        donationManager.leafRend2.material.color = CurrentColor;
    }
    
    public void OnTeal()
    {
        CurrentColor = Teal;
        donationManager.leafRend1.material.color = CurrentColor;
        donationManager.leafRend2.material.color = CurrentColor;
    }
    
    public void OnGold()
    {
        CurrentColor = Gold;
        donationManager.leafRend1.material.color = CurrentColor;
        donationManager.leafRend2.material.color = CurrentColor;
    }
    
    public void OnRed()
    {
        CurrentColor = red;
        donationManager.leafRend1.material.color = CurrentColor;
        donationManager.leafRend2.material.color = CurrentColor;

    }
    
    public void OnPurple()
    {
        CurrentColor = purple;
        donationManager.leafRend1.material.color = CurrentColor;
        donationManager.leafRend2.material.color = CurrentColor;

    }
}
