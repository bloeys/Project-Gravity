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

    private void Start()
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

        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);

        if (Input.mouseScrollDelta.y < 0)
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + zoomSpd, 400, 10000);
        else if (Input.mouseScrollDelta.y > 0)
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - zoomSpd, 400, 10000);
    }
}