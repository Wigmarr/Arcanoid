using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Ball : NetworkBehaviour
{
    [SerializeField] private float strength = 3f;
    private Vector3 prevMoveDir = Vector3.zero;
    private Vector3 prevPos;
    private Vector3 moveDir = Vector3.zero;
    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
   {
        Collider bounceSurface = collision.collider;

        if (!isServerOnly)
        {
            if (bounceSurface != null)
            {
                if (collision.gameObject.tag == "Block")
                {
                    GameObject localPlayer = ClientScene.localPlayer.gameObject;
                    if (localPlayer != null) localPlayer.GetComponent<PlayerMovement>().DestroyOnServer(collision.gameObject);
                }
                if (prevMoveDir == Vector3.zero)
                {

                    moveDir = collision.GetContact(0).normal.normalized;


                    // Debug.Log("Bounce Surf: " + bounceSurface.velocity);
                    return;
                }
                moveDir = Vector3.Reflect(prevMoveDir, collision.GetContact(0).normal).normalized;
            }
        }
        
  
        

    }
    private void Start()
    {
        prevPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        prevMoveDir = transform.position - prevPos;
        prevPos = transform.position;
        rb.MovePosition(transform.position + moveDir * strength);
    }
}
