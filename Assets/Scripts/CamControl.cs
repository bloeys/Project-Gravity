using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CamControl : MonoBehaviour
{
    public Transform target;
    public float zoomSpd = 20;
    Camera cam;
    int index = 0;

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index++;
            if (index == GravityController.bodies.Count)
                index = 0;

            target = GravityController.bodies[index].transform;
        }

        else if (Input.GetMouseButtonDown(1))
        {
            index--;
            if (index < 0)
                index = GravityController.bodies.Count - 1;

            target = GravityController.bodies[index].transform;
        }

        //Follow target
        transform.position = new Vector3(target.position.x, target.position.y + cam.farClipPlane / 2, target.position.z);

        //Zoom and camera y-position
        if (Input.mouseScrollDelta.y < 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
                transform.position += Vector3.up * zoomSpd;
            else
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + zoomSpd, 400, 10000);
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
                transform.position -= Vector3.up * zoomSpd;
            else
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - zoomSpd, 400, 10000);
        }
    }
}