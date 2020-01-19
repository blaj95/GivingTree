using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donations : MonoBehaviour
{
    public List<Donation> donations;

    private void Start()
    {
        donations = new List<Donation>();
        Donation donation1 = new Donation(35, "Brodey Lajoie", "It's a very sad time for Australia. Every dollar counts so please donate!");
        Donation donation2 = new Donation(60, "Kexin Cha", "I'm happy to help this cause. And on such a cool app too!");
        Donation donation3 = new Donation(25, "Lauren Kam", "Praying for all who are affected by these wildfires. Sending all I can to help out the people, animals, and fauna, Also, loving the tree!");
        donations.Add(donation1);
        donations.Add(donation2);
        donations.Add(donation3);
    }
}
