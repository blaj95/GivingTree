using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ChooseCause : MonoBehaviour
{
    public GameObject arCamera, scanTip;
    public ARPlaneManager planeManager;
    public TreePlacement treePlacement;
    public GameObject tracker;
    
    public void OnCauseChoosen()
    {
        planeManager.enabled = true;
        scanTip.SetActive(true);
        tracker.SetActive(true);
        treePlacement.enabled = true;
        gameObject.SetActive(false);
    }
}
