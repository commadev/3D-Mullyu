using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    float speed;

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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.new_cube_red.transform.Translate(Vector3.right * speed /* * Time.smoothDeltaTime */, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.new_cube_red.transform.Translate(Vector3.right * speed /* * Time.smoothDeltaTime */ * -1, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.new_cube_red.transform.Translate(Vector3.forward * speed /* * Time.smoothDeltaTime */, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.new_cube_red.transform.Translate(Vector3.forward * speed /* * Time.smoothDeltaTime */ * -1, Space.World);
        }
        /*
        this.new_cube_red.transform.Translate(Vector3.right * speed * Time.smoothDeltaTime);
        this.new_cube_blue.transform.Translate(Vector3.right * speed * Time.smoothDeltaTime);
        this.new_cube_green.transform.Translate(Vector3.right * speed * Time.smoothDeltaTime);
        this.new_cube_yellow.transform.Translate(Vector3.right * speed * Time.smoothDeltaTime);
        */
    }
}

/*
 * if (Input.GetKey(KeyCode.DownArrow))
 */
