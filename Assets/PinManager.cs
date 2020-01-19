using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public GameObject PinPrefab;
    public GameObject Earth;

    public Vector2[] coordinates;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < coordinates.Length; i++)
        {
            PlacePin(coordinates[i].x, coordinates[i].y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void PlacePin(float longitude, float latitude)
	{

        GameObject newPin = Instantiate(PinPrefab, Earth.transform) as GameObject;
		newPin.transform.localPosition = new Vector3(0f, 0f, -0.5f);

        newPin.transform.RotateAround(Earth.transform.TransformPoint(Vector3.zero), Earth.transform.TransformDirection(Vector3.right), latitude);
        //newPin.transform.RotateAround(Earth.transform.TransformPoint(Vector3.zero), Earth.transform.TransformDirection(Vector3.up), longitude);

        newPin.transform.rotation *= Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), newPin.transform.localPosition);
        
        
	}
}
