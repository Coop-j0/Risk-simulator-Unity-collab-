using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    //variable for rotation functionality
    [Header("Rotation Variables")]
    public float rotationSpeed;

    //variables for zoom functionality
    [Header("Zoom Variables")]
    public int zoomSens = 2;
    public int defaultFov = 60;
    public int maxFov = 60;
    public int minFov = 20;

    //vairables for click+drag functionality
    [Header("Click and Drag Variables")]
    public float dragSpeed = 0.01f;
    private Vector3 dragOrigin;
    private Vector3 defultCameraPos = new Vector3(0, 1.918f, 0);
    private float minXPos = -2.0f;
    private float maxXPos = 2.0f;
    private float minZPos = -0.6f;
    private float maxZPos = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.fieldOfView = defaultFov;
        GameObject.Find("Camera Target").transform.position = defultCameraPos;
    }

    // Update is called once per frame
    void Update()
    {
        //keybinds for camera manipulation
        bool rotateClockwise = Input.GetKey(KeyCode.Q);
        bool rotateAntiClockwise = Input.GetKey(KeyCode.E);
        float zoomInOut = Input.GetAxis("Mouse ScrollWheel");
        bool clickToDrag = Input.GetMouseButtonDown(1);
        bool resetCamera = Input.GetMouseButton(2);
        
        //rotation functionality
        if (rotateClockwise)
        {
            transform.Rotate(Vector3.up, 1 * rotationSpeed * Time.deltaTime);
        }
        else if (rotateAntiClockwise)
        {
            transform.Rotate(Vector3.up, -1 * rotationSpeed * Time.deltaTime);
        }

        //zoom functionality
        if (zoomInOut > 0 && Camera.main.fieldOfView > minFov)
        {
            Camera.main.fieldOfView -= zoomSens;
        }
        else if (zoomInOut < 0 && Camera.main.fieldOfView < maxFov)
        {
            Camera.main.fieldOfView += zoomSens;
        }

        //click and drag functionality
        if (clickToDrag)
        {
            dragOrigin = Input.mousePosition;
            return;
        }
        
        if (resetCamera)
        {
            GameObject.Find("Camera Target").transform.position = defultCameraPos;
            GameObject.Find("Camera Target").transform.rotation = Quaternion.Euler(0, 0, 0);
            Camera.main.fieldOfView = defaultFov;
            return;
        }

        if (!Input.GetMouseButton(1) && !Input.GetMouseButton(2)) 
            return;

        //restricts space in which camera can move
        bool outOfBoundsX = false;
        bool outOfBoundsNX = false;
        bool outOfBoundsZ = false;
        bool outOfBoundsNZ = false;

        if (GameObject.Find("Camera Target").transform.position.x > maxXPos)
        {
            outOfBoundsX = true; 
        }
        if (GameObject.Find("Camera Target").transform.position.x < minXPos)
        {
            outOfBoundsNX = true;
        }
        if (GameObject.Find("Camera Target").transform.position.z > maxZPos)
        {
            outOfBoundsZ = true;
        }
        if (GameObject.Find("Camera Target").transform.position.z < minZPos)
        {
            outOfBoundsNZ = true;
        }

        if (outOfBoundsNX)
        {
            float zPos = GameObject.Find("Camera Target").transform.position.z;
            Vector3 quickReset = new Vector3(minXPos, 1.918f, zPos);
            GameObject.Find("Camera Target").transform.position = quickReset;
        }
        else if (outOfBoundsX)
        {
            float zPos = GameObject.Find("Camera Target").transform.position.z;
            Vector3 quickReset = new Vector3(maxXPos, 1.918f, zPos);
            GameObject.Find("Camera Target").transform.position = quickReset;
        }
        else if (outOfBoundsNZ)
        {
            float xPos = GameObject.Find("Camera Target").transform.position.x;
            Vector3 quickReset = new Vector3(xPos, 1.918f, minZPos);
            GameObject.Find("Camera Target").transform.position = quickReset;
        }
        else if (outOfBoundsZ)
        {
            float xPos = GameObject.Find("Camera Target").transform.position.x;
            Vector3 quickReset = new Vector3(xPos, 1.918f, maxZPos);
            GameObject.Find("Camera Target").transform.position = quickReset;
        }
        else
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

            transform.Translate(move, Space.World);
        }
    }
}
