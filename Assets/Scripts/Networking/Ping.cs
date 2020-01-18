using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ping : MonoBehaviourPun
{
    public TMP_Text pingText;
    public GameObject tree;
    public Transform target;
    public GameObject leafPrefab;
    public GameObject leafPrefabLocal;
    public void SendPing()
    {
        photonView.RPC("PingRPC", RpcTarget.Others, photonView.ViewID.ToString());
    }

    public void SpawnLeaf(float r, float g, float b, Vector3 pos)
    {
        photonView.RPC("CubeColorRPC", RpcTarget.Others,  r,  g,  b,pos);
        GameObject leaf = Instantiate(leafPrefabLocal,pos,Quaternion.identity);
        leaf.GetComponent<Renderer>().material.color = new Color(r, g, b);
        // leaf.transform.parent = target;
    }

    [PunRPC]
    public void PingRPC(string id)
    {
        pingText.text += "Message Received from " + id;
    }
    
    [PunRPC]
    public void CubeColorRPC(float r, float g, float b, Vector3 pos)
    {
        GameObject leaf = Instantiate(leafPrefab,pos,Quaternion.identity);
        leaf.GetComponent<Renderer>().material.color = new Color(r, g, b);
        // leaf.transform.parent = target;
    }

    void Update()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            Ray raycastTap = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycastTap, out raycastHit))
            {
                if (raycastHit.transform.CompareTag("Tree"))
                {
                    Debug.Log("Hit");
                    SpawnLeaf(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), raycastHit.point);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // SpawnLeaf(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            Debug.Log("Color change");
        }
    }
}
    