using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class Controller : MonoBehaviour
{
    int animationCount = 1000;

    string[] result;
    Socket sock;
    string cmd;
    byte[] receiverBuff;
    ObjectPos[] mObjectPos;
    ObjectPos[] mPreObjectPos;

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

        protected virtual ObjectPos DeepCopy()
        {
            ObjectPos other = (ObjectPos)this.MemberwiseClone();
            other.posX = getPosX();
            other.posY = getPosY();
            other.posDesX = getPosDesX();
            other.posDesY = getPosDesY();
            return other;
        }

        public ObjectPos Clone()
        {
            return DeepCopy();
        }

        public void printAllPos(int x)
        {
            Debug.Log(x + " posX : " + posX + " posY : " + posY + " desPosX : " + posDesX + " desPosY : " + posDesY);
        }
    }

    GameObject cube_red;
    GameObject cube_blue;
    GameObject cube_green;
    GameObject cube_yellow;

    GameObject dst_red;
    GameObject dst_blue;
    GameObject dst_green;
    GameObject dst_yellow;

    void Start()
    {
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        sock.Connect(ep);

        cmd = string.Empty;
        receiverBuff = new byte[8192];

        this.cube_red = GameObject.Find("Cube_Red");
        this.cube_green = GameObject.Find("Cube_Green");
        this.cube_blue = GameObject.Find("Cube_Blue");
        this.cube_yellow = GameObject.Find("Cube_Yellow");

        this.dst_red = GameObject.Find("Dst_Red");
        this.dst_green = GameObject.Find("Dst_Green");
        this.dst_blue = GameObject.Find("Dst_Blue");
        this.dst_yellow = GameObject.Find("Dst_Yellow");

        mObjectPos = new ObjectPos[4];
        mPreObjectPos = new ObjectPos[4];

        mPreObjectPos[0] = new ObjectPos(0, 0, 0, 0);
        mPreObjectPos[1] = new ObjectPos(0, 0, 0, 0);
        mPreObjectPos[2] = new ObjectPos(0, 0, 0, 0);
        mPreObjectPos[3] = new ObjectPos(0, 0, 0, 0);
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

        /*
        if( Math.Abs(mObjectPos[0].getPosX() - mPreObjectPos[0].getPosX()) == 1 || Math.Abs(mObjectPos[0].getPosY() - mPreObjectPos[0].getPosY()) == 1 ||
            Math.Abs(mObjectPos[1].getPosX() - mPreObjectPos[1].getPosX()) == 1 || Math.Abs(mObjectPos[1].getPosY() - mPreObjectPos[1].getPosY()) == 1 ||
            Math.Abs(mObjectPos[2].getPosX() - mPreObjectPos[2].getPosX()) == 1 || Math.Abs(mObjectPos[2].getPosY() - mPreObjectPos[2].getPosY()) == 1 ||
            Math.Abs(mObjectPos[3].getPosX() - mPreObjectPos[3].getPosX()) == 1 || Math.Abs(mObjectPos[3].getPosY() - mPreObjectPos[3].getPosY()) == 1)
        {
            for (int i=0; i<animationCount;i++)
            {
                if(mObjectPos[0].getPosX() - mPreObjectPos[0].getPosX() == 0)
                    this.cube_red.transform.Translate(Vector3.forward * (1 / animationCount) * (mObjectPos[0].getPosY() - mPreObjectPos[0].getPosY()));
                else
                    this.cube_red.transform.Translate(Vector3.right * (1 / animationCount) * (mObjectPos[0].getPosX() - mPreObjectPos[0].getPosX()));

                if (mObjectPos[0].getPosX() - mPreObjectPos[0].getPosX() == 0)
                    this.cube_green.transform.Translate(Vector3.forward * (1 / animationCount) * (mObjectPos[1].getPosY() - mPreObjectPos[1].getPosY()));
                else
                    this.cube_green.transform.Translate(Vector3.right * (1 / animationCount) * (mObjectPos[1].getPosX() - mPreObjectPos[1].getPosX()));

                if (mObjectPos[0].getPosX() - mPreObjectPos[0].getPosX() == 0)
                    this.cube_blue.transform.Translate(Vector3.forward * (1 / animationCount) * (mObjectPos[2].getPosY() - mPreObjectPos[2].getPosY()));
                else
                    this.cube_blue.transform.Translate(Vector3.right * (1 / animationCount) * (mObjectPos[2].getPosX() - mPreObjectPos[2].getPosX()));

                if (mObjectPos[0].getPosX() - mPreObjectPos[0].getPosX() == 0)
                    this.cube_yellow.transform.Translate(Vector3.forward * (1 / animationCount) * (mObjectPos[3].getPosY() - mPreObjectPos[3].getPosY()));
                else
                    this.cube_yellow.transform.Translate(Vector3.right * (1 / animationCount) * (mObjectPos[3].getPosX() - mPreObjectPos[3].getPosX()));

            }
        }
        else
        {
            this.cube_red.transform.SetPositionAndRotation(Vector3.right * mObjectPos[0].getPosX() + Vector3.forward * mObjectPos[0].getPosY() + Vector3.up * 1, Quaternion.identity);
            this.cube_green.transform.SetPositionAndRotation(Vector3.right * mObjectPos[1].getPosX() + Vector3.forward * mObjectPos[1].getPosY() + Vector3.up * 1, Quaternion.identity);
            this.cube_blue.transform.SetPositionAndRotation(Vector3.right * mObjectPos[2].getPosX() + Vector3.forward * mObjectPos[2].getPosY() + Vector3.up * 1, Quaternion.identity);
            this.cube_yellow.transform.SetPositionAndRotation(Vector3.right * mObjectPos[3].getPosX() + Vector3.forward * mObjectPos[3].getPosY() + Vector3.up * 1, Quaternion.identity);
        }
        */

        /*
        mPreObjectPos[0] = mObjectPos[0].Clone();
        mPreObjectPos[1] = mObjectPos[1].Clone();
        mPreObjectPos[2] = mObjectPos[2].Clone();
        mPreObjectPos[3] = mObjectPos[3].Clone();
        */

        this.cube_red.transform.SetPositionAndRotation(Vector3.right * mObjectPos[0].getPosX() + Vector3.forward * mObjectPos[0].getPosY() + Vector3.up * 1, Quaternion.identity);
        this.cube_green.transform.SetPositionAndRotation(Vector3.right * mObjectPos[1].getPosX() + Vector3.forward * mObjectPos[1].getPosY() + Vector3.up * 1, Quaternion.identity);
        this.cube_blue.transform.SetPositionAndRotation(Vector3.right * mObjectPos[2].getPosX() + Vector3.forward * mObjectPos[2].getPosY() + Vector3.up * 1, Quaternion.identity);
        this.cube_yellow.transform.SetPositionAndRotation(Vector3.right * mObjectPos[3].getPosX() + Vector3.forward * mObjectPos[3].getPosY() + Vector3.up * 1, Quaternion.identity);

        this.dst_red.transform.SetPositionAndRotation(Vector3.right * mObjectPos[0].getPosDesX() + Vector3.forward * mObjectPos[0].getPosDesY() + Vector3.up * 0.5f, Quaternion.identity);
        this.dst_green.transform.SetPositionAndRotation(Vector3.right * mObjectPos[1].getPosDesX() + Vector3.forward * mObjectPos[1].getPosDesY() + Vector3.up * 0.5f, Quaternion.identity);
        this.dst_blue.transform.SetPositionAndRotation(Vector3.right * mObjectPos[2].getPosDesX() + Vector3.forward * mObjectPos[2].getPosDesY() + Vector3.up * 0.5f, Quaternion.identity);
        this.dst_yellow.transform.SetPositionAndRotation(Vector3.right * mObjectPos[3].getPosDesX() + Vector3.forward * mObjectPos[3].getPosDesY() + Vector3.up * 0.5f, Quaternion.identity);
    }
}