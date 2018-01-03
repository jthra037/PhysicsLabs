using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
    }

    private void Update()
    {
        
    }

    private Vector3 WhichIsFarther()
    {
        float sub1 = (transform.position - pos1).sqrMagnitude;
        float sub2 = (transform.position - pos2).sqrMagnitude;

        return sub1 > sub2 ? pos1 : pos2;
    }

    private bool ShouldIChangeCourse()
    {
        float sub1 = (transform.position - pos1).sqrMagnitude;
        float sub2 = (transform.position - pos2).sqrMagnitude;

        float epsilon = (rb.velocity * Time.fixedDeltaTime).sqrMagnitude;

        return sub1
    }

    private void FixedUpdate()
    {
        
    }
}
