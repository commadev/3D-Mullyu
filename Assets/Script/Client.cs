using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client : MonoBehaviour
{
    Socket sock;
    string cmd;
    byte[] receiverBuff;
    // Start is called before the first frame update
    void Start()
    {
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var ep = new IPEndPoint(IPAddress.Parse("175.122.48.163"), 8080);
        sock.Connect(ep);

        cmd = string.Empty;
        receiverBuff = new byte[8192];

        Debug.Log("Connected...");
    }

    // Update is called once per frame
    void Update()
    {
        int n = sock.Receive(receiverBuff);

        string data = Encoding.UTF8.GetString(receiverBuff, 0, n);

        Debug.Log(data);
    }
}