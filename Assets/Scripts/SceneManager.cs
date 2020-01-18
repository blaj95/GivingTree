using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject uiCamera, arCamera, targetImage;
    public void RoomJoined()
    {
        uiCamera.SetActive(false);
        arCamera.SetActive(true);
        targetImage.SetActive(true);
    }
}
