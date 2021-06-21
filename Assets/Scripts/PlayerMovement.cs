using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private Camera camera = null;
    [SerializeField] private GameObject shield = null;
    private Vector2 previousInput;
    private Quaternion cameraDefRot;
    private InputSystem controls;

    private InputSystem Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new InputSystem();
        }
    }

    private void Awake()
    {

    }
    public override void OnStartAuthority()
    {
        if (isLocalPlayer)
        {
            
            enabled = true;
            Controls.Player.Movement.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
            Controls.Player.Movement.canceled += ctx => ResetMovement();
            camera.enabled = true;
            camera.gameObject.SetActive(true);
            camera.tag = "MainCamera";
        }

    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();
    [ClientCallback]
    private void Update() {
        if (isLocalPlayer)
        {
            Move();
            Turn(controls.Player.Look.ReadValue<Vector2>());
        }
    }
    [ClientCallback]
    private void FixedUpdate()
    {
        
    }
    [Client]
    private void SetMovement(Vector2 movement) => previousInput = movement;
    [Client]
    private void ResetMovement() => previousInput = Vector2.zero;
    [Client]
    private void Turn(Vector2 lookAt)
    {
       // Debug.Log("LookAt: " + lookAt);
        Vector3 screenPoint = new Vector3(lookAt.x, transform.position.y, lookAt.y);
        Vector3 mouseWorld = camera.ScreenToWorldPoint(screenPoint);
        
        Vector3 dir = mouseWorld - transform.position;
        float angle = Mathf.Atan2(dir.z, dir.x);
        var rot = Quaternion.AngleAxis(lookAt.x, Vector3.up);
       // Debug.Log("LookAtWorld: " + rot);
        //rb.MoveRotation(rb.rotation*rot);
        transform.rotation = this.transform.rotation*rot;
        //shield.GetComponent<Rigidbody>().MoveRotation(rot);
        
    }
    [Client]
    public void DestroyOnServer(GameObject obj)
    {
        CmdDestroyObject(obj);
    }

    [Client]
    private void Move()
    {
        Vector3 right = transform.right;
        Vector3 forward = transform.forward;

        right.y = 0f;
        forward.y = 0f;

        Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;
        Debug.Log("Player: " + movement);
        controller.Move(movement * movementSpeed * Time.deltaTime);
    }
    
    [Command]
    private void CmdDestroyObject(GameObject obj)
    {
        NetworkServer.Destroy(obj);
    }
}
