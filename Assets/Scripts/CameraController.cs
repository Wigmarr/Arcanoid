using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    Quaternion rot;
    void Awake()
    {
        rot = transform.rotation;
    }


    private void FixedUpdate()
    {
        //transform.rotation = rot;
    }


}
