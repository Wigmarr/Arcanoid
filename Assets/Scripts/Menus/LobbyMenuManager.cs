using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class LobbyMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton = null;
    [SerializeField] private TMP_Text[] playerNameText = new TMP_Text[10];
    [SerializeField] private GameObject[] playerReadyBackground = new GameObject[10];
    [SerializeField] private Button disconnectButton = null;
    [SerializeField] private Button readyButton = null;
    public delegate void Click();
    
    public static event Click OnReadyClicked;
    public static event Click OnDisconnectClicked;
    public static event Click OnStartClicked;
    public static LobbyMenuManager instance = null;
    // Start is called before the first frame update


    public void ReadyClicked()
    {
        OnReadyClicked?.Invoke();
    }

    public void DisconnectClicked()
    {
        OnDisconnectClicked?.Invoke();
    }

    public void StartClicked()
    {
        OnStartClicked?.Invoke();
    }

    public void UpdateReadyState(int index, bool readyState)
    {

        playerReadyBackground[index].GetComponent<Image>().color = readyState ? Color.green : Color.red;
    }

    public void UpdatePlayerName(int index, string playerName)
    {
        playerNameText[index].GetComponent<TMP_Text>().text = playerName;
    }

    void Awake()
    {
        if (instance == null)
        { // Экземпляр менеджера был найден
            instance = this; // Задаем ссылку на экземпляр объекта
        }
        else if (instance == this)
        { // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }
    }

    void Start()
    {
        startButton.gameObject.SetActive(false);
    }

    public void setStartActive(bool b)
    {
        startButton.gameObject.SetActive(b);
    }

    void Update()
    {

    }
}

