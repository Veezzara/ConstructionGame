using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public float distance;
    public float speed;
    public float zoomSpeed;
    public Text FPSCounter;
    GameObject[] blocks;

    void Start()
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");
        for (int i = 0; i < blocks.Length; i++)
        {
            Destroy(blocks[i]);
        }
    }

    void Update () {
        FPSCounter.text = "FPS: " + (1f / Time.deltaTime).ToString("F2");
        Vector3 targetPos = target.transform.position;
        blocks = GameObject.FindGameObjectsWithTag("Block");
        for (int i = 0; i < blocks.Length; i++)
        {
            targetPos = targetPos + blocks[i].transform.position;
        }
        targetPos = targetPos / (blocks.Length + 1);
        transform.position = targetPos - transform.forward * distance;
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * speed, 0) * Time.deltaTime, Space.World);
            transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * speed, 0, 0) * Time.deltaTime);

            transform.position = targetPos - transform.forward * distance;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (distance >= 5)
            {
                distance = distance / zoomSpeed;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            distance = distance * zoomSpeed;
        }
    }
}
