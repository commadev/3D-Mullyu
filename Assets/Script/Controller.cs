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
        public int getPosY()
        {
            return posY;
        }

        public void printAllPos(int x)
        {
            Debug.Log(x + " posX : " + posX);
            Debug.Log(x + " posY : " + posY);
            Debug.Log(x + " desPosX : " + desPosX);
            Debug.Log(x + " desPosY : " + desPosY);
        }
    }

    GameObject cube_red;
    GameObject cube_blue;
    GameObject cube_green;
    GameObject cube_yellow;

    GameObject new_cube;
    Vector3 positionValue;

    GameObject new_cube_red;
    GameObject new_cube_blue;
    GameObject new_cube_green;
    GameObject new_cube_yellow;

    void Start()
    {
        mObjectPos = new ObjectPos[4];

        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var ep = new IPEndPoint(IPAddress.Parse("175.122.48.163"), 8080);
        sock.Connect(ep);

        cmd = string.Empty;
        receiverBuff = new byte[8192];

        this.cube_red = GameObject.Find("Cube_Red");
        this.cube_blue = GameObject.Find("Cube_Blue");
        this.cube_green = GameObject.Find("Cube_Green");
        this.cube_yellow = GameObject.Find("Cube_Yellow");

        positionValue = new Vector3(Random.Range(-10.0f, 10.0f), 1, Random.Range(-10.0f, 10.0f));
        new_cube = (GameObject)Instantiate(cube_red, positionValue, Quaternion.identity);
        positionValue = new Vector3(Random.Range(-10.0f, 10.0f), 1, Random.Range(-10.0f, 10.0f));
        new_cube = (GameObject)Instantiate(cube_blue, positionValue, Quaternion.identity);
        positionValue = new Vector3(Random.Range(-10.0f, 10.0f), 1, Random.Range(-10.0f, 10.0f));
        new_cube = (GameObject)Instantiate(cube_green, positionValue, Quaternion.identity);
        positionValue = new Vector3(Random.Range(-10.0f, 10.0f), 1, Random.Range(-10.0f, 10.0f));
        new_cube = (GameObject)Instantiate(cube_yellow, positionValue, Quaternion.identity);

        this.new_cube_red = GameObject.Find("Cube_Red(Clone)");
        this.new_cube_blue = GameObject.Find("Cube_Blue(Clone)");
        this.new_cube_green = GameObject.Find("Cube_Green(Clone)");
        this.new_cube_yellow = GameObject.Find("Cube_Yellow(Clone)");

        speed = new_cube.transform.localScale.x;
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
 
        this.new_cube_red.transform.SetPositionAndRotation(Vector3.right * mObjectPos[0].getPosX() + Vector3.forward * mObjectPos[0].getPosY(), Quaternion.identity);
        this.new_cube_green.transform.SetPositionAndRotation(Vector3.right * mObjectPos[1].getPosX() + Vector3.forward * mObjectPos[1].getPosY(), Quaternion.identity);
        this.new_cube_blue.transform.SetPositionAndRotation(Vector3.right * mObjectPos[2].getPosX() + Vector3.forward * mObjectPos[2].getPosY(), Quaternion.identity);
        this.new_cube_yellow.transform.SetPositionAndRotation(Vector3.right * mObjectPos[3].getPosX() + Vector3.forward * mObjectPos[3].getPosY(), Quaternion.identity);
    }
}
