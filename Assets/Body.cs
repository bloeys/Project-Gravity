using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Body : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public Vector3 initialVel;
    public Vector2 massLimits;
    public float velLimit;

    [Range(1, 20)]
    public float massMulti = 1;
    public bool randomize = true;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (!randomize)
        {
            rb.mass = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
            rb.mass *= massMulti;
            rb.velocity = initialVel;
        }

        else
        {
            rb.mass = Random.Range(massLimits.x, massLimits.y);
            rb.velocity = new Vector3(Random.Range(-velLimit, velLimit), 0, Random.Range(-velLimit, velLimit));
        }
        GravityController.AddToBodies(this);
    }
}