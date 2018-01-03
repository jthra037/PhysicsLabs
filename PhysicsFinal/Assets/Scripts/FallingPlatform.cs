using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingPlatform : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private float fallDelay = 2.5f;

    private MovingPlatform movementController;

    private bool falling = false;


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        movementController = GetComponent<MovingPlatform>();

        rb.useGravity = false;

        if (movementController == null)
        {
            rb.isKinematic = true;
        }
	}

    IEnumerator delayThenFall()
    {
        Coroutine wobbler = StartCoroutine(wobble());

        yield return new WaitForSeconds(fallDelay);

        rb.isKinematic = false;
        falling = true;

        StopCoroutine(wobbler);
        if (movementController != null)
        {
            Destroy(movementController);
        }
        Debug.Log("Falling!");
    }

    IEnumerator wobble()
    {
        while (true)
        {
            float pitch = Random.Range(-5, 5);
            float yaw = Random.Range(-5, 5);
            float roll = Random.Range(-5, 5);

            //transform.rotation = Quaternion.Euler(pitch, yaw, roll);

            yield return new WaitForSeconds(0.15f);
        }
    }

    private void FixedUpdate()
    {
        if (falling)
        {
            rb.AddForce(3 * Physics.gravity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Falling in " + fallDelay);
            StartCoroutine(delayThenFall());
        }
    }

}
