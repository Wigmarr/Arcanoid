using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterNameMenu : MonoBehaviour
{
    [SerializeField] private Button EnterNameButton = null;
    [SerializeField] private TMP_InputField EnterNameInputField = null;

    private const string playerName = "PlayerName";

    public static string DisplayName { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(playerName)) { return; }
        string defaultName = PlayerPrefs.GetString(playerName);
        EnterNameInputField.text = defaultName;
        SetPlayerName(defaultName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerName(string name)
    {
        EnterNameButton.interactable = !string.IsNullOrEmpty(name);
    }
    public void SavePlayerName()
    {
        DisplayName = EnterNameInputField.text;
        PlayerPrefs.SetString(playerName, DisplayName);
    }
}
