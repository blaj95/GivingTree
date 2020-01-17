using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields

    [SerializeField]
    private byte maxPlayersPerRoom = 10;
    
#endregion

        
#region Private Fields

        string gameVersion = "1";


#endregion


#region MonoBehaviour CallBacks
        
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Connect now happening on Button Click instead of 
    // void Start()
    // {
    //     Connect();
    // }


#endregion

#region MonoBehaviourPunCallbacks Callbacks


public override void OnConnectedToMaster()
{
    Debug.Log("OnConnectedToMaster() was called by PUN");
    PhotonNetwork.JoinRandomRoom();
}


public override void OnDisconnected(DisconnectCause cause)
{
    Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
}

public override void OnJoinRandomFailed(short returnCode, string message)
{
    Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
    PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });}

public override void OnJoinedRoom()
{
    if (PhotonNetwork.IsMasterClient)
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room and is the master client."); 
    }
    else
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
    }
}


#endregion

#region Public Methods

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    
#endregion

}
