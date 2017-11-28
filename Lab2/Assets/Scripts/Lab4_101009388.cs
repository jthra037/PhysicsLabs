using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab4_101009388 : MonoBehaviour {

    private bool isOnGround;
    private Rigidbody rb;

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
            PillarController otherController = hit.transform.gameObject.GetComponentInParent<PillarController>();
            float distance = hit.distance;
            float height = hit.transform.lossyScale.y;
            float speed = otherRB.velocity.z;

            Debug.Log(distance);
            Debug.Log(height);
            Debug.Log(speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
