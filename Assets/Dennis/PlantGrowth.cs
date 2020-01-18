using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{

    private bool growing;
    private float growthDelay; //how long after the growth event does this plant start growing?
    private float growthStartTime;

    private Vector3 newScale;
    private Vector3 newEuler;

    public float growthDuration = 2f;


    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (growing && (Time.time > growthStartTime + growthDelay) )
        {
            print("print");
            float growthProportion = (Time.time - (growthStartTime + growthDelay)) / growthDuration;
            if(growthProportion >= 1f)
            {
                growing = false;
            }
            else
            {
                print("scaling");
                transform.localScale = Vector3.Lerp(transform.localScale, newScale, growthProportion);
                //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, newEuler, growthProportion);
            }
        }
        
    }

    public void Grow(float scale, float delay)
    {
        growthStartTime = Time.time;
        growing = true;
        newScale = new Vector3(scale, scale, scale);
        newEuler = new Vector3(0f, transform.localEulerAngles.y + Random.Range(30f, 200f), 0f);
        growthDelay = delay;
        print(newScale);
       
    }
}
