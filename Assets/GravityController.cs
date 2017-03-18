using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public static List<Body> bodies { get; private set; }
    public float gravityConstant = 10;
    public float timeScale = 1;

    void Update()
    {
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        for (int i = 0; i < bodies.Count; i++)
        {
            for (int j = i + 1; j < bodies.Count; j++)
            {
                Vector3 force = bodies[j].transform.position - bodies[i].transform.position;
                float dist = force.magnitude;
                force.Normalize();
                force *= gravityConstant * bodies[i].rb.mass * bodies[j].rb.mass;
                force /= dist * dist;

                bodies[i].rb.AddForce(force, ForceMode.Force);
                bodies[j].rb.AddForce(-force, ForceMode.Force);
            }
        }
    }

    public static void AddToBodies(Body b)
    {
        if (bodies == null)
            bodies = new List<Body>();
        bodies.Add(b);
    }

    void OnDrawGizmos()
    {
        if (bodies == null) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < bodies.Count; i++)
            if (bodies != null && bodies[i] != null)
                Gizmos.DrawLine(bodies[i].transform.position, bodies[i].transform.position + bodies[i].rb.velocity);
    }
}