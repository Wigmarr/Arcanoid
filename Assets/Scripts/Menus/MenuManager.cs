using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject panelLogin = null;
    [SerializeField] private GameObject panelMenu = null;
    [SerializeField] private GameObject panelIp = null;
    // Start is called before the first frame update
    void Start()
    {
        ResetUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetUI()
    {
        panelIp.SetActive(false);
        panelMenu.SetActive(false);
        panelLogin.SetActive(true);
    }
}
