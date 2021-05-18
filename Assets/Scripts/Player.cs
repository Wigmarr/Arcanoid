using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{

    [SyncVar(hook = nameof(OnHolaCountChange))]
    int holaCount = 0;


    private InputSystem controls = null;
    [SerializeField] private float movementSpeed = 5f;
    private void Awake() => controls = new InputSystem();
    void OnEnable() => controls.Player.Enable();
    void OnDisable() => controls.Player.Disable();


    void Update() {
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

    void Move()
    {
        var movementInput = controls.Player.Movement.ReadValue<Vector2>();

        var movement = new Vector3();
        movement.x = movementInput.x;
        movement.z = movementInput.y;

        movement.Normalize();

        transform.Translate(movement * movementSpeed * Time.deltaTime);

    }
}
