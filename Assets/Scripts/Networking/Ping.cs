using System.Collections;
using System.Collections.Generic;
using Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Ping : MonoBehaviourPun
{
    public TMP_Text pingText;
    
    public void SendPing()
    {
       photonView.RPC("PingRPC",RpcTarget.Others,photonView.ViewID.ToString());
    }

    [PunRPC]
    public void PingRPC(string id)
    {
        pingText.text += "Message Received from " + id;
    }
}
