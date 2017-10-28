using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    private float vert;
    private float hori;
    private GameObject inRange;

    [SerializeField]
    private float maxSpeed = 10;

    [HideInInspector]
    public Rigidbody rb;

    public float accel = 1;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        vert = Input.GetAxisRaw("Vertical");
        hori = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift) && inRange)
        {
            inRange.transform.parent = transform;
            inRange.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && inRange)
        {
            inRange.transform.parent = null;
            inRange.GetComponent<Rigidbody>().isKinematic = false;
            inRange = null;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(hori, rb.velocity.y / accel, vert) * accel * Time.fixedDeltaTime, ForceMode.VelocityChange);
        if (rb.velocity.sqrMagnitude > (maxSpeed * maxSpeed))
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            inRange = other.gameObject;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Rock"))
    //    {
    //        inRange = null;
    //    }
    //}
}
