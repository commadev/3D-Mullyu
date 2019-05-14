using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client : MonoBehaviour
{
    string[] result;
    Socket sock;
    string cmd;
    byte[] receiverBuff;
    ObjectPos[] mObjectPos;

    class ObjectPos
    {
        private int posX;
        private int posY;

        private int desPosX;
        private int desPosY;

        public ObjectPos(int x, int y, int xd, int yd)
        {
            posX = x;
            posY = y;
            desPosX = xd;
            desPosY = yd;
        }

        public int getPosX()
        {
            return posX;
        }

        public void printAllPos(int t)
        {
            Debug.Log("posX : " + posX);
            Debug.Log("posY : " + posY);
            Debug.Log("desPosX : " + desPosX);
            Debug.Log("desPosY : " + desPosY);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mObjectPos = new ObjectPos[4];

        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var ep = new IPEndPoint(IPAddress.Parse("175.122.48.163"), 8080);
        sock.Connect(ep);

        cmd = string.Empty;
        receiverBuff = new byte[8192];
    }

    // Update is called once per frame
    void Update()
    {
        int n = sock.Receive(receiverBuff);
        string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
        result = data.Split(':');

        for (int i = 0; i <= 12; i += 4)
        {
            mObjectPos[i / 4] = new ObjectPos(int.Parse(result[i]), int.Parse(result[i + 1]), int.Parse(result[i + 2]), int.Parse(result[i + 3]));
            mObjectPos[i / 4].printAllPos(i / 4);
        }
    }
}