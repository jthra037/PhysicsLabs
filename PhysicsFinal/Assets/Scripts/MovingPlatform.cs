using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    private Vector3 pos1;
    private Vector3 pos2;

    private Vector3 currentTarget;

    [SerializeField] private float speed = 3;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.freezeRotation = true;

        pos1 = target1.position;
        pos2 = target2.position;

        currentTarget = WhichIsFarther();
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

        return sub1 - epsilon <= 0 || sub2 - epsilon <= 0;
    }

    private void FixedUpdate()
    {
        if (ShouldIChangeCourse())
        {
            currentTarget = WhichIsFarther();
        }

        rb.velocity = (currentTarget - transform.position).normalized * speed;
    }
}
