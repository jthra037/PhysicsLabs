using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab4_101009388 : MonoBehaviour {

    private bool isOnGround;
    private Rigidbody rb;
    private int jumps = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Rigidbody otherRB = hit.transform.gameObject.GetComponentInParent<Rigidbody>();
         
            float distance = hit.distance;
            float height = hit.transform.lossyScale.y;
            float speed = otherRB.velocity.z;

            //Debug.Log(distance);
            //Debug.Log(height);
            //Debug.Log(speed);

            float jumpVi = findVi(0, Physics.gravity.y, height + 1);
            float jumpT = findT(0, jumpVi, Physics.gravity.y);
            float postJumpDistance = findNextD(distance, speed, jumpT);

            //Debug.Log(jumpVi);
            //Debug.Log(jumpT);
            //Debug.Log(postJumpDistance);

            if (postJumpDistance < 0.45f && isOnGround && jumps == 0)
            {
                rb.AddForce(transform.up * jumpVi, ForceMode.VelocityChange);
                jumps++;
                Debug.Log("Jumping!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = true;
            jumps = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

    private float findVi(float vf, float a, float d)
    {
        return Mathf.Sqrt((vf * vf) - (2 * a * d));
    }

    private float findT(float vf, float vi, float a)
    {
        return (vf - vi) / a;
    }

    private float findNextD(float d, float v, float t)
    {
        return d + (v * t);
    }
}
