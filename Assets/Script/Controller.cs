using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Controller : MonoBehaviour
{

    float speed;

    string[] result;
    Socket sock;
    string cmd;
    byte[] receiverBuff;
    ObjectPos[] mObjectPos;

    class ObjectPos
    {
        private int posX;
        private int posY;

        private int posDesX;
        private int posDesY;

        public ObjectPos(int x, int y, int xd, int yd)
        {
            posX = x;
            posY = y;
            posDesX = xd;
            posDesY = yd;
        }

        public int getPosX()
        {
            return posX;
        }
        public int getPosY()
        {
            return posY;
        }
        public int getPosDesX()
        {
            return posDesX;
        }
        public int getPosDesY()
        {
            return posDesY;
        }

        public void printAllPos(int x)
        {
            Debug.Log(x + " posX : " + posX);
            Debug.Log(x + " posY : " + posY);
            Debug.Log(x + " desPosX : " + posDesX);
            Debug.Log(x + " desPosY : " + posDesY);
        }
    }

    GameObject cube_red;
    GameObject cube_blue;
    GameObject cube_green;
    GameObject cube_yellow;

    GameObject spot_red;
    GameObject spot_blue;
    GameObject spot_green;
    GameObject spot_yellow;

    void Start()
    {
        mObjectPos = new ObjectPos[4];

        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        sock.Connect(ep);

        cmd = string.Empty;
        receiverBuff = new byte[8192];

        this.cube_red = GameObject.Find("Cube_Red");
        this.cube_green = GameObject.Find("Cube_Green");
        this.cube_blue = GameObject.Find("Cube_Blue");
        this.cube_yellow = GameObject.Find("Cube_Yellow");

        this.spot_red = GameObject.Find("Spot_Red");
        this.spot_green = GameObject.Find("Spot_Green");
        this.spot_blue = GameObject.Find("Spot_Blue");
        this.spot_yellow = GameObject.Find("Spot_Yellow");

        speed = cube_red.transform.localScale.x;
    }

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

        this.cube_red.transform.SetPositionAndRotation(Vector3.right * mObjectPos[0].getPosX() + Vector3.forward * mObjectPos[0].getPosY(), Quaternion.identity);
        this.cube_green.transform.SetPositionAndRotation(Vector3.right * mObjectPos[1].getPosX() + Vector3.forward * mObjectPos[1].getPosY(), Quaternion.identity);
        this.cube_blue.transform.SetPositionAndRotation(Vector3.right * mObjectPos[2].getPosX() + Vector3.forward * mObjectPos[2].getPosY(), Quaternion.identity);
        this.cube_yellow.transform.SetPositionAndRotation(Vector3.right * mObjectPos[3].getPosX() + Vector3.forward * mObjectPos[3].getPosY(), Quaternion.identity);

        this.spot_red.transform.SetPositionAndRotation(Vector3.right * mObjectPos[0].getPosDesX() + Vector3.forward * mObjectPos[0].getPosDesY() + Vector3.up * 5, Quaternion.Euler(90, 0, 0));
        this.spot_green.transform.SetPositionAndRotation(Vector3.right * mObjectPos[1].getPosDesX() + Vector3.forward * mObjectPos[1].getPosDesY() + Vector3.up * 5, Quaternion.Euler(90, 0, 0));
        this.spot_blue.transform.SetPositionAndRotation(Vector3.right * mObjectPos[2].getPosDesX() + Vector3.forward * mObjectPos[2].getPosDesY() + Vector3.up * 5, Quaternion.Euler(90, 0, 0));
        this.spot_yellow.transform.SetPositionAndRotation(Vector3.right * mObjectPos[3].getPosDesX() + Vector3.forward * mObjectPos[3].getPosDesY() + Vector3.up * 5, Quaternion.Euler(90, 0, 0));
    }
}