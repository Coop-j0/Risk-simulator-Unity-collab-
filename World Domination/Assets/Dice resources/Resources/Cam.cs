using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    void Start(){
        rb = target.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float yRot = -Mathf.Acos(Vector3.Dot(rb.velocity.normalized, Vector3.right)) * Mathf.Rad2Deg;
        target.eulerAngles = new Vector3(0, yRot, 0);
    }
}
