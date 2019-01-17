using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    private int _connectionCounter = 0;

    // Server callbacks
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (_connectionCounter > 1)
        {
            //Possibility for spectators;
            return;
        }
        var player = (GameObject)GameObject.Instantiate(spawnPrefabs[_connectionCounter], spawnPrefabs[_connectionCounter].transform.position, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        _connectionCounter++;
    }
}
