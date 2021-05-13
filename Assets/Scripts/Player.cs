using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    
    [SyncVar(hook = nameof(OnHolaCountChange))]
    int holaCount = 0;
    void HandleMovement() {
        if (isLocalPlayer) {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position = transform.position + movement;
        }
    }
    
    void Update() {
        HandleMovement();

        if (isLocalPlayer && Input.GetKeyDown("x")) {
            Debug.Log("Sending Hola to Server");
            Hola();
        }
    }

    [Command]
    void Hola() {
        Debug.Log("Received Hola from Client~!");
        holaCount++;
        ReplyHola();
    }

    [TargetRpc]
    void ReplyHola() {
        Debug.Log("Received Hola form Server!");
    }
    
    [ClientRpc]
    void TooHigh() {
        Debug.Log("Too high!");
    }

    void OnHolaCountChange(int oldCount, int newCount) {
        Debug.Log($"We had {oldCount} holas, but now we have {newCount}");
    }
}
