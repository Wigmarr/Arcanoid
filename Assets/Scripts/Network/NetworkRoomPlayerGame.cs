using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Mirror;
using System;

public class NetworkRoomPlayerGame : NetworkRoomPlayer
{
    static readonly ILogger logger = LogFactory.GetLogger(typeof(NetworkRoomPlayerGame));

    public override void OnStartLocalPlayer()
    {

        if (isServer)
        {
            LobbyMenuManager.instance.setStartActive(false);
        }
        CmdChangeReadyState(false);
        name = PlayerPrefs.GetString("PlayerName");
        LobbyMenuManager.OnReadyClicked += changeState;
        LobbyMenuManager.OnDisconnectClicked += disconectPlayer;
        LobbyMenuManager.OnStartClicked += startGame;
        UpdateUI();
    }

    void startGame()
    {
        Room.ServerChangeScene(Room.GameplayScene);
    }

    public override void OnStartClient()
    {
        if (logger.LogEnabled()) logger.LogFormat(LogType.Log, "OnStartClient {0}", SceneManager.GetActiveScene().path);
        
    }



    void changeState()
    {

        CmdChangeReadyState(!readyToBegin);

    }

    void disconectPlayer()
    {
        if (isClient) NetworkManager.singleton.StopClient();
        else if (isServer) NetworkManager.singleton.StopHost();
    }

    public override void OnClientEnterRoom()
    {
        if (logger.LogEnabled()) logger.LogFormat(LogType.Log, "OnClientEnterRoom {0}", SceneManager.GetActiveScene().path);
        UpdateUI();

    }

    public override void OnClientExitRoom()
    {
        if (logger.LogEnabled()) logger.LogFormat(LogType.Log, "OnClientExitRoom {0}", SceneManager.GetActiveScene().path);
    }

    public override void ReadyStateChanged(bool _, bool newReadyState)
    {
        UpdateUI();
        if (logger.LogEnabled()) logger.LogFormat(LogType.Log, "ReadyStateChanged {0}", newReadyState);

    }

    private NetworkManagerGame room;

    private NetworkManagerGame Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerGame;
        }
    }

    public void UpdateUI()
    {
        foreach (var player in Room.roomSlots)
        {
            LobbyMenuManager.instance.UpdatePlayerName(player.index, player.name);
            LobbyMenuManager.instance.UpdateReadyState(player.index, player.readyToBegin);
        }
    }


}
