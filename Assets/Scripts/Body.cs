using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Body : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public Vector3 oldPos { get; private set; }

    [SerializeField]
    Material[] mats;

    public Vector3 initialVel;
    public bool randomize = true;
    public Vector2 massLimits;
    public float velLimit;
    [Range(1, 20)]
    public float massMulti = 1;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        int index = Random.Range(-1, mats.Length);
        if (index != -1 && mats.Length > 0)
            GetComponent<MeshRenderer>().material = mats[Random.Range(0, mats.Length)];

        if (!randomize)
        {
            rb.mass = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
            rb.mass *= massMulti;
            rb.velocity = initialVel;
        }

        else
        {
            rb.mass = Random.Range(massLimits.x, massLimits.y) * Random.Range(1, massMulti);
            rb.velocity = new Vector3(Random.Range(-velLimit, velLimit), Random.Range(-velLimit, velLimit), Random.Range(-velLimit, velLimit));
        }
        GravityController.AddToBodies(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        Body b = collision.gameObject.GetComponent<Body>();

        //If the distance between the surfaces of the two bodies is less than a certain thing then don't show effects
        if ((oldPos - b.oldPos).magnitude - transform.localScale.x / 2 - b.transform.localScale.x / 2 < 10f) return;

        float impactForce = Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity);

        if (impactForce > 50)
            ParticlesManager.PlaySystem(collision.contacts[0].point, (int)impactForce, impactForce);
    }

    public void UpdateOldPos()
    {
        oldPos = transform.position;
    }
}