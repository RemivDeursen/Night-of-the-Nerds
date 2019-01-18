using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    public Transform PlayerObjectsSpawnPoint;
    private int _connectionCounter = 2;

    // Server callbacks
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (_connectionCounter > 2)
        {
            //Possibility for spectators;
            return;
        }
        Debug.Log("Instantiating  prefab " + _connectionCounter);
        var player = (GameObject)GameObject.Instantiate(spawnPrefabs[_connectionCounter], PlayerObjectsSpawnPoint.position, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        _connectionCounter++;
    }
}
