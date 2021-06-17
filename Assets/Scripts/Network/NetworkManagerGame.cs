using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class NetworkManagerGame: NetworkRoomManager
{
    [Header("Spawner Setup")]
    [Tooltip("PowerUp Prefab for the Spawner")]
    [SerializeField] private GameObject powerUpPrefab;
    public override void OnRoomServerSceneChanged(string sceneName)
    {
        // spawn the initial batch of Rewards
        if (sceneName == RoomScene)
        {
        
         //   LobbyMenuManager.OnStartClicked += startGame;
        }
       
        if (sceneName == GameplayScene)
        {

            // Spawner.InitialSpawn();
        }
    }
  

    public override void OnRoomClientSceneChanged(NetworkConnection conn)
    {
        if (IsSceneActive(RoomScene))
        {
            
        }
    }

    public override void OnRoomServerConnect(NetworkConnection conn)
    {

        if (numPlayers >= maxConnections) { conn.Disconnect(); return; }

    }



    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
    {
        //PlayerScore playerScore = gamePlayer.GetComponent<PlayerScore>();
        //playerScore.index = roomPlayer.GetComponent<NetworkRoomPlayer>().index;
        return true;
    }

    public override void OnRoomStopClient()
    {
        // Demonstrates how to get the Network Manager out of DontDestroyOnLoad when
        // going to the offline scene to avoid collision with the one that lives there.

        if (gameObject.scene.name == "DontDestroyOnLoad" && !string.IsNullOrEmpty(offlineScene) && SceneManager.GetActiveScene().path != offlineScene)
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }

    public override void OnRoomStopServer()
    {
        // Demonstrates how to get the Network Manager out of DontDestroyOnLoad when
        // going to the offline scene to avoid collision with the one that lives there.
        if (gameObject.scene.name == "DontDestroyOnLoad" && !string.IsNullOrEmpty(offlineScene) && SceneManager.GetActiveScene().path != offlineScene)
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

        base.OnRoomStopServer();
    }

    public override void OnRoomStartHost()
    {
 
        ServerChangeScene(RoomScene);
        
    }

    public override void OnRoomServerAddPlayer(NetworkConnection conn)
    { 
        base.OnRoomServerAddPlayer(conn);
    }
    public override void OnRoomClientConnect(NetworkConnection conn)
    {
        base.OnRoomClientConnect(conn);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public override void OnRoomClientDisconnect(NetworkConnection conn)
    {
        base.OnRoomClientDisconnect(conn);

    }

    public override void OnRoomServerPlayersReady()
    {

        if (allPlayersReady && roomSlots[0].isLocalPlayer){
            LobbyMenuManager.instance.setStartActive(true);
        }
    }

    

    public override void OnRoomServerPlayersNotReady()
    {
        if (roomSlots[0].isLocalPlayer)
        {
            LobbyMenuManager.instance.setStartActive(false);
        }
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
    }



}
