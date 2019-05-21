using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotate = 20.0f;

    // public float mouseSensitivity = 2f;
    // private float rotY;
    // private float rotX;

    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        CameraMoving();
        CameraRotate();
        CameraUpDown();
    }

    void CameraMoving()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.smoothDeltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.smoothDeltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back *  speed * Time.smoothDeltaTime);
        }
    }

    void CameraRotate()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.back * rotate * Time.smoothDeltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward * rotate * Time.smoothDeltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down * rotate * Time.smoothDeltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * rotate * Time.smoothDeltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.right * rotate * Time.smoothDeltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.left * rotate * Time.smoothDeltaTime);
        }
        /*
        rotY = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotX = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        transform.localRotation *= Quaternion.Euler(-rotX, 0, 0);
        */
    }

    void CameraUpDown()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * speed * Time.smoothDeltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * speed * Time.smoothDeltaTime, Space.World);
        }
    }
}