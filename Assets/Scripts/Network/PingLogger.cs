using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Mirror;

public class PingLogger : NetworkBehaviour
{
    private float time = 0.0f;
    private string path;
    private 
    // Start is called before the first frame updat
    void Start()
    {
        if (!Directory.Exists(Directory.GetCurrentDirectory()+@"\Logs"))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Logs");
        }
        path = Directory.GetCurrentDirectory() + @"\Logs";
    }

    public void WritePing()
    {
        if (!File.Exists(path +@"\" + ClientScene.localPlayer.netId + ".txt"))
            File.Create(path + @"\" + ClientScene.localPlayer.netId + ".txt").Dispose();
        int ping = (int)(NetworkTime.rtt * 1000);
        File.AppendAllText(path + @"\" + ClientScene.localPlayer.netId + ".txt", "" + ping + "\n");
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1.0f)
        {
            WritePing();
            time = 0f;
        }
    }
}
