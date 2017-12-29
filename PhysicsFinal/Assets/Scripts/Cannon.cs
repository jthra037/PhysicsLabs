using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    [SerializeField] private GameObject target;
    private Rigidbody targetRB;
    //private Vector3 targetPosition;

    private bool isLoaded = false;
    private Rigidbody ammoRB = null;

	// Use this for initialization
	void Start ()
    {
        targetRB = target.GetComponent<Rigidbody>();
	}

    public void Fire()
    {
        Vector3 muzzleVelocity = FIN.FindTrajectoryVelocity(transform.position,
            target.transform.position,
            targetRB == null ? Vector3.zero : targetRB.velocity);

        ammoRB.isKinematic = false;
        ammoRB.AddForce(muzzleVelocity, ForceMode.VelocityChange);

        isLoaded = false;
        ammoRB = null;
    }
	


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerRef = other.GetComponent<PlayerController>();
            ammoRB = playerRef.GetComponent<Rigidbody>();
            ammoRB.isKinematic = true;
            playerRef.isInCannon = true;
            playerRef.loadedCannon = this;

            isLoaded = true;

            other.transform.position = transform.position;
        }
    }
}
