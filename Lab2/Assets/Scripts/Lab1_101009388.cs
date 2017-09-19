using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Lab1_101009388 : MonoBehaviour {

    // Use this for initialization
    private Rigidbody rb;
    private bool isAccelerating = false;
    [SerializeField]
    private float acceleration = 10.72f;
    private float elapsedTime = 0;
    private float speed1;

    [SerializeField]
    private GameObject wallObject;

    [SerializeField]
    private float zeroToHundredTime = Mathf.NegativeInfinity;



    void Start () {
        rb = GetComponent<Rigidbody>();

        if(zeroToHundredTime != Mathf.NegativeInfinity)
        {
            /* d = (vf + vi /2) * t //vi = 0
             * d = (vf/2) * t //vf = 100km/h
             * d = 50 * t //t is in seconds though
             * d = (50 * 3.6) * t
             */

            /*1000m/1km * 3600s/1h*/

            float displacement =  Vector3.Distance((transform.position + (transform.forward * ((50 / 3.6f) * zeroToHundredTime))), transform.position);
            acceleration = (100 / 3.6f) / (zeroToHundredTime);
            Instantiate(wallObject, transform.position + (transform.forward * ((50 / 3.6f) * zeroToHundredTime)), transform.rotation);
            Debug.Log("Acceleration will be: " + acceleration + "m/ss");
            Debug.Log("Displacement will be: " + displacement + "m");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isAccelerating)
        {
            if (speed1 != rb.velocity.magnitude)
            {
                Debug.Log("acceleration: " + ((rb.velocity.magnitude - speed1) / Time.fixedDeltaTime) + "m/ss");
                Debug.Log("velocity: " + (rb.velocity.magnitude * 3.6f) + "km/h");
            }
        }
        else
        {
            isAccelerating = Input.GetKeyDown(KeyCode.Space);
        }
        speed1 = rb.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        if (isAccelerating)
        {
            
            elapsedTime += Time.fixedDeltaTime;
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.AddForce((transform.up * 10) + (2 * transform.forward * -rb.velocity.magnitude), ForceMode.VelocityChange);
        Debug.Log("You made it to 100 km/hour in: " + elapsedTime + "s");
        isAccelerating = false;
    }
}