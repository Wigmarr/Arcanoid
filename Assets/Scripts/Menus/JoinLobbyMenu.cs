using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerGame networkManager = null;
    [SerializeField] private TMP_InputField ipAddressField = null;
    [SerializeField] private Button joinButton = null;

    public void JoinLobby()
    {
        string ipAddress = ipAddressField.text;
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();
        joinButton.interactable = false;
    }

}
